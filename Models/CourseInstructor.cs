using System;
using System.Collections.Generic;

#nullable disable

namespace ASPNETCore5Demo.Models
{
    public partial class CourseInstructor : UscEntityModelBase
    {
        public int CourseId { get; set; }
        public int InstructorId { get; set; }

        [UscField(UscFieldAttrValues.ReadOnly)]
        public virtual Course Course { get; set; }

        [UscField(UscFieldAttrValues.ReadOnly)]
        public virtual Person Instructor { get; set; }
    }

    public class CourseInstructorData
    {
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
    }
}
