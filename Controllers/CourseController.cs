using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Controllers
{
    public class CourseController : MarkDeletedControllerBase<ContosoUniversityContext, Course>
    {
        public CourseController(ContosoUniversityContext db)
            : base(db, nameof(db.Courses))
        { }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourses() => InnerGetList();

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseById(int id) => InnerGetByKeys(id);

        [HttpPost("")]
        public ActionResult<Course> PostCourse(CourseData data) => InnerPost(data, nameof(GetCourseById), new { id = data.CourseId });

        [HttpPut("")]
        public IActionResult PutCourse(CourseData data) => InnerPutByKeys(data, data.CourseId);

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourseById(int id) 
            //=> InnerDeleteByKeys(id);
            => InnerMarkDeletedByKeys(id);
    }
}
