using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Controllers
{
    public class CourseInstructorsController : UscControllerBase<ContosoUniversityContext, CourseInstructor>
    {
        public CourseInstructorsController(ContosoUniversityContext db)
            : base(db, nameof(db.CourseInstructors))
        { }

        [HttpGet("")]
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInstructors() => InnerGetList();

        [HttpGet("courseId, instructorId")]
        public ActionResult<CourseInstructor> GetCourseInstructorById(int courseId, int instructorId) 
            => InnerGetByKeys(new object[] { courseId, instructorId });
            
        [HttpPost("courseId, instructorId")]
        public ActionResult<CourseInstructor> PostCourseInstructor(int courseId, int instructorId) 
            => InnerPost(new CourseInstructor() { CourseId= courseId, InstructorId = instructorId } , nameof(GetCourseInstructorById), new { courseId, instructorId });

        //[HttpPut("")]
        //public IActionResult PutCourseInstructor(CourseInstructorData data)
        //{
        //    //return InnerPut(model);
        //    return InnerPutByKeys(data, new object[] { data.CourseId, data.InstructorId });
        //}

        [HttpPut("courseId, instructorId")]
        public IActionResult PutCourseInstructor(int courseId, int instructorId, CourseInstructorData data)
            => InnerPutByKeys(data, new object[] { courseId, instructorId }); //可以改 key 嗎??

        [HttpDelete("courseId, instructorId")]
        public ActionResult<CourseInstructor> DeleteCourseInstructorById(int courseId, int instructorId)
            => InnerDeleteByKeys(new object[] { courseId, instructorId });        
    }
}

