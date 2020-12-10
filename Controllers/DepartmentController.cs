using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[Department]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public ContosoUniversityContext db { get; }
        public DepartmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return db.Departments.AsNoTracking().ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            return null;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Course>> GetDepartmentCourses(int id)
        {
            //var dept = db.Departments.Include(p => p.cou)

            return db.Courses.Where(p => p.DepartmentId==id).ToArray();
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
