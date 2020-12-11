using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpDepartmentController : ControllerBase
    {
        public ContosoUniversityContext db { get; }
        public SpDepartmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        //[HttpGet("{id}")]
        //public ActionResult<Department> GetDepartmentById(int id)
        //{
        
        //}

        [HttpPost("")]
        public ActionResult<DepartmentVersion> PostDepartment(DepartmentData d)
        {
            var pName = new SqlParameter("Name", d.Name);
            var pBudget = new SqlParameter("Budget", d.Budget);
            var pStartDate = new SqlParameter("StartDate", d.StartDate);
            var pInstructorID = new SqlParameter("InstructorID", d.InstructorId);

            var d2 = db.DepartmentVersion.FromSqlRaw("EXECUTE dbo.Department_Insert  @Name, @Budget, @StartDate, @InstructorID",
                pName, pBudget, pStartDate, pInstructorID).AsEnumerable().First();

            return CreatedAtAction(null, null, d2);
        }

        [HttpPut("{id}")]
        public IActionResult PutCourse(DepartmentData data)
        {
            var d = db.Departments.Find(data.DepartmentId);
            if (d == null)
                return NotFound();

            var pId = new SqlParameter("DepartmentID", d.DepartmentId);
            var pName = new SqlParameter("Name", data.Name);
            var pBudget = new SqlParameter("Budget", data.Budget);
            var pStartDate = new SqlParameter("StartDate", data.StartDate);
            var pInstructorID = new SqlParameter("InstructorID", data.InstructorId);
            var pVer = new SqlParameter("RowVersion_Original", d.RowVersion);
            db.Database.ExecuteSqlRaw("EXECUTE dbo.Department_Update @DepartmentID, @Name, @Budget, @StartDate, @InstructorID, @RowVersion_Original",
                pId, pName, pBudget, pStartDate, pInstructorID, pVer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteById(int id)
        {
            var d = db.Departments.Find(id);
            if (d == null)
                return NotFound();

            var pId = new SqlParameter("DepartmentID", d.DepartmentId);
            var pVer = new SqlParameter("RowVersion_Original", d.RowVersion);
            db.Database.ExecuteSqlRaw("EXECUTE dbo.Department_Delete @DepartmentID,@RowVersion_Original", pId, pVer);
            return d;
        }
    }
}