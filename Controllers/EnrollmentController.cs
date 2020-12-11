using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Controllers
{
    public class EnrollmentController : UscControllerBase<ContosoUniversityContext, Enrollment>
    {
        public EnrollmentController(ContosoUniversityContext db)
            : base(db, nameof(db.Enrollments))
        { }

        [HttpGet("")]
        public ActionResult<IEnumerable<Enrollment>> GetEnrollments() => InnerGetList();

        [HttpGet("{id}")]
        public ActionResult<Enrollment> GetEnrollmentById(int id) => InnerGetByKeys(id);

        [HttpPost("")]
        public ActionResult<Enrollment> PostEnrollment(EnrollmentData data) => InnerPost(data, nameof(GetEnrollmentById), new { id = data.EnrollmentId });

        [HttpPut("")]
        public IActionResult PutEnrollment(EnrollmentData data) => InnerPutByKeys(data, data.EnrollmentId);

        [HttpDelete("{id}")]
        public ActionResult<Enrollment> DeleteEnrollmentById(int id) => InnerDeleteByKeys(id);
    }
}
