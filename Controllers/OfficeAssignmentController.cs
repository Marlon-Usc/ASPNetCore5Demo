using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Controllers
{
    public class OfficeAssignmentController : UscControllerBase<ContosoUniversityContext, OfficeAssignment>
    {
        public OfficeAssignmentController(ContosoUniversityContext db)
            : base(db, nameof(db.OfficeAssignments))
        { }

        [HttpGet("")]
        public ActionResult<IEnumerable<OfficeAssignment>> GetOfficeAssignments() => InnerGetList();

        [HttpGet("{id}")]
        public ActionResult<OfficeAssignment> GetOfficeAssignmentById(int id) => InnerGetByKeys(id);

        [HttpPost("")]
        public ActionResult<OfficeAssignment> PostOfficeAssignment(OfficeAssignmentData data) => InnerPost(data, nameof(GetOfficeAssignmentById), new { id = data.InstructorId });

        [HttpPut("")]
        public IActionResult PutOfficeAssignment(OfficeAssignmentData data) => InnerPutByKeys(data, data.InstructorId);

        [HttpDelete("{id}")]
        public ActionResult<OfficeAssignment> DeleteOfficeAssignmentById(int id) => InnerDeleteByKeys(id);
    }
}
