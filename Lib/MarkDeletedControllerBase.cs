using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo
{
    public abstract class MarkDeletedControllerBase<TDbContext, TEntity> : UscControllerBase<TDbContext, TEntity> where TDbContext : DbContext where TEntity : UscEntityModelBase
    {
        protected MarkDeletedControllerBase(TDbContext Db, string dbSetName)
            :base(Db, dbSetName)
        { }

        protected virtual ActionResult<TEntity> InnerMarkDeletedByKeys(params object[] keyValues)
        {
            TEntity data = InnerGetDataByKeys(HttpVerbs.Delete, keyValues);
            if (data == null) 
                return NotFound();
            
            var prop = typeof(TEntity).GetProperty("IsDeleted");
            if (prop != null)
            {
                prop.SetValue(data, true);
                Db.SaveChanges();
            }
            return Ok();
        }
    }
}
