using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        public ContosoUniversityContext db { get; }
        public CoursesController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return db.Courses.ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            var c = db.Courses.Find(id);
            if (c == null)
                return NotFound();
            return c;
        }

        [HttpGet("Credits={Credits}")]
        public ActionResult<IEnumerable<Course>> GetCourseByCredits(int Credits)
        {
            return  db.Courses.Where(p => p.Credits == Credits).ToArray();
        }
        
        [HttpPost("")]
        public ActionResult<Course> PostCourse(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
            return CreatedAtAction("GetCourseById",new {id=course.CourseId} , course);
        }

        [HttpPut("{id}")]
        public IActionResult PutCourse(int id, CourseForUpdate course)
        {   
            var c = db.Courses.Find(id);
            if (c == null)
                return NotFound();
            c.UpdateFrom(course);
            db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteTModelById(int id)
        {
            //先 select 到 cache, 將 cache 標註為 deleted, SaveChanges() 時 update 到 DB
            var course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            db.Courses.Remove(course);

            //直接在 cache 新增一筆並標註為 deleted, SaveChanges() 時 update 到 DB
            // Course cDel = new Course() { CourseId = id };
            // db.Entry(cDel).State = EntityState.Deleted;

            // ef 不適合批次作業, 批量作業還是要 直接下 SQL
            //db.Database.ExecuteSqlRaw($"Delete from db.Course where  CourseId = {id} ");

            db.SaveChanges();
            return course;
        }
    }
}