using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo
{
    public abstract class UscControllerBase<TDbContext, TEntity> : ControllerBase where TDbContext : DbContext where TEntity : UscEntityModelBase
    {
        protected TDbContext Db { get; }
        protected DbSet<TEntity> DbSet { get; }

        protected UscControllerBase(TDbContext Db, DbSet<TEntity> model)
        {
            this.Db = Db;
            this.DbSet = model;
        }

        protected ActionResult<IEnumerable<TEntity>> GetList()
        {
            return DbSet.AsNoTracking().ToArray();
        }

        public ActionResult<TEntity> GetByKey(params object[] keyValues)
        {
            return null;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Course>> GetDepartmentCourses(int id)
        {
            //var dept = Db.Departments.Include(p => p.cou)

            return Db.Courses.Where(p => p.DepartmentId == id).ToArray();
        }

        // [HttpPost("")]
        // public ActionResult<Department> PostDepartment(Department model)
        // {
        //     return null;
        // }

        // [HttpPut("{id}")]
        // public IActionResult PutDepartment(int id, Department model)
        // {
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public ActionResult<Department> DeleteDepartmentById(int id)
        // {
        //     return null;
        // }
    }
}
