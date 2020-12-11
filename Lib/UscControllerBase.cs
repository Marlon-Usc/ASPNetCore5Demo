using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class UscControllerBase<TDbContext, TEntity> : ControllerBase where TDbContext : DbContext where TEntity : UscEntityModelBase
    {
        protected TDbContext Db { get; }
        protected DbSet<TEntity> DbSet { get; }

        protected UscControllerBase(TDbContext Db, string dbSetName)            
        {
            this.Db = Db;
            var prop = this.Db.GetType().GetProperty(dbSetName);
            if (prop == null)
                throw new ArgumentException("Not Found!", nameof(dbSetName));
            this.DbSet = prop.GetValue(Db) as DbSet<TEntity>;            
        }

        protected virtual ActionResult<IEnumerable<TEntity>> InnerGetList()
        {
            if (typeof(IIsDeleted).IsAssignableFrom(typeof(TEntity) ))
                return DbSet.Where(e => !true.Equals(((IIsDeleted)e).IsDeleted)).ToArray();
            return DbSet.AsNoTracking().ToArray();
        }

        protected virtual TEntity InnerGetDataByKeys(HttpVerbs verb, params object[] keyValues)
        {
            TEntity data = DbSet.Find(keyValues);
            return (data == null || (data is IIsDeleted deleted && true.Equals(deleted.IsDeleted))) ? null : data;
        }

        protected virtual ActionResult<TEntity> InnerGetByKeys(params object[] keyValues)
        {
            var data = InnerGetDataByKeys(HttpVerbs.Get, keyValues);
            if (data == null)
                return NotFound();
            return data;
        }
                        
        protected virtual ActionResult<TEntity> InnerPost(object data, string getActionName, object routeValues)
        {
            TEntity toIns = (TEntity)typeof(TEntity).GetConstructor(Array.Empty<Type>()).Invoke(null);
            toIns.UpdateFrom(data);
            DbSet.Add(toIns);
            Db.SaveChanges();
            return CreatedAtAction(getActionName,routeValues, data);
        }

        //[HttpPost("")]
        //public ActionResult<TEntity> Post(TEntity data)
        //{            
        //    return InnerPost(data, nameof(GetByPK), new { keySet = data.GetKeyValueSet() });
        //}

        protected virtual IActionResult InnerPutByKeys(object data, params object[] keyValues)
        {
            if (keyValues == null || keyValues.Length == 0)
            {
                if (data is not TEntity oldData)
                {
                    oldData = (TEntity)typeof(TEntity).GetConstructor(Array.Empty<Type>()).Invoke(null);
                    oldData.UpdateFrom(data);
                }
                Db.Entry(oldData).State = EntityState.Modified;
            }
            else
            {
                var oldData = InnerGetDataByKeys(HttpVerbs.Put, keyValues);
                if (oldData == null)
                    return NotFound();
                oldData.UpdateFrom(data);
            }
            Db.SaveChanges();
            return NoContent();
        }

        protected virtual IActionResult InnerPut(TEntity data)
        {
            return InnerPutByKeys(data, UscDbContextHelper.GetKeys<TEntity>(Db, data));
        }
        
        protected virtual ActionResult<TEntity> InnerDeleteByKeys(params object[] keyValues)
        {
            if (typeof(IIsDeleted).IsAssignableFrom(typeof(TEntity)))
                return InnerMarkDeletedByKeys(keyValues);

            //先 select 到 cache, 將 cache 標註為 deleted, SaveChanges() 時 update 到 DB            
            var data = InnerGetDataByKeys(HttpVerbs.Delete, keyValues);
            if (data == null) return NotFound();            
            DbSet.Remove(data);

            //直接在 cache 新增一筆並標註為 deleted, SaveChanges() 時 update 到 DB
            //TEntity toDel = (TEntity)typeof(TEntity).GetConstructor(Array.Empty<Type>()).Invoke(null);
            //toDel.SetKeys(keyValues);            
            //Db.Entry(toDel).State = EntityState.Deleted; => 如果已經被 Find 到 cache 會有 key 值重覆的問題,
            // => 分別建立 for Select/Insert/Update/Delete 4 個 model
            // 看能不能都先 insert 到 #tmptable 再批次更新

            // ef 不適合批次作業, 批量作業還是要 直接下 SQL
            //db.Database.ExecuteSqlRaw($"Delete from db.Course where  CourseId = {id} ");

            Db.SaveChanges();
            return Ok();
        }

        protected virtual ActionResult<TEntity> InnerMarkDeletedByKeys(params object[] keyValues)
        {
            IIsDeleted data = (IIsDeleted)InnerGetDataByKeys(HttpVerbs.Delete, keyValues);
            if (data == null)
                return NotFound();

            data.IsDeleted = true;
            Db.SaveChanges();
            return Ok();
        }
    }


    [Flags]
    public enum HttpVerbs
    {
        Get = 1,
        Delete = 8,
        Post = 2,
        Put = 4,
        Head = 16,
        Patch = 32,
        Options = 64
    }
}
