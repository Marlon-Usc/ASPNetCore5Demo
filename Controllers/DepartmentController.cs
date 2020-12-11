using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Controllers
{
    public class DepartmentController : UscControllerBase<ContosoUniversityContext, Department>
    {        
        public DepartmentController(ContosoUniversityContext db)
            : base(db, nameof(db.Departments))
        { }

        [HttpGet("")]
        public ActionResult<IEnumerable<Department>> GetDepartments() => InnerGetList();

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id) => InnerGetByKeys(id);

        [HttpPost("")]
        public ActionResult<Department> PostDepartment(DepartmentData data) => InnerPost(data, nameof(GetDepartmentById), new { id = data.DepartmentId });

        [HttpPut("")]
        public IActionResult PutDepartment(DepartmentData data) => InnerPutByKeys(data, data.DepartmentId);

        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteDepartmentById(int id)
            //=> InnerDeleteByKeys(id);
            => InnerMarkDeletedByKeys(id);


        [HttpGet("CourseCount={id}")]
        public ActionResult<VwDepartmentCourseCount> GetCourseCountById(int id)
        {
            return Db.VwDepartmentCourseCounts.FromSqlRaw<VwDepartmentCourseCount>
                   ($"SELECT DepartmentID, Name, CourseCount FROM dbo.VwDepartmentCourseCount where DepartmentId={id}").First< VwDepartmentCourseCount>();
        }

        //[HttpGet("ddl")]
        //public ActionResult<IEnumerable<DepartmentDropDown>> GetDepartmentDropDown()
        //{
        //    // return db.Database.SqlQuery<DepartmentDropDown>($"SELECT DepartmentID, Name FROM dbo.Department").ToList();

        //    // return db.Departments.Select(p => new DepartmentDropDown() {
        //    //     DepartmentId = p.DepartmentId,
        //    //     Name = p.Name
        //    // }).ToList();

        //    return Db.DepartmentDropDown.FromSqlInterpolated($"SELECT DepartmentID, Name FROM dbo.Department").ToList();
        //}

        //[HttpGet("{id}")]
        //public ActionResult<IEnumerable<Course>> GetDepartmentCourses(int id)
        //{
        //    var dept = Db.Departments.Include(p => p.Courses)
        //                 .First(p => p.DepartmentId == id);

        //    return dept.Courses.ToList();

        //    // return Db.Courses.Where(p => p.DepartmentId == id).ToList();
        //}
    }
}
