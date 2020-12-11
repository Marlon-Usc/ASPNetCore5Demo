using System;
using System.Collections.Generic;

#nullable disable

namespace ASPNETCore5Demo.Models
{
    public partial class OfficeAssignment : UscEntityModelBase
    {
        [UscField(UscFieldAttrValues.ReadOnly)]
        public int InstructorId { get; set; }
        public string Location { get; set; }

        [UscField(UscFieldAttrValues.ReadOnly)]
        public virtual Person Instructor { get; set; }
    }

    public class OfficeAssignmentData
    {
        public int InstructorId { get; set; }
        public string Location { get; set; }
    }
}
