using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ASPNETCore5Demo.Models
{
    public partial class Person : UscEntityModelBase
    {
        public Person()
        {
            CourseInstructors = new HashSet<CourseInstructor>();
            Departments = new HashSet<Department>();
            Enrollments = new HashSet<Enrollment>();
        }

        [UscField(UscFieldAttrValues.ReadOnly)]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string Discriminator { get; set; }

        public DateTime DateModified { get; set; }
        public bool IsDeleted { get; set; }

        [UscField(UscFieldAttrValues.ReadOnly)]
        [JsonIgnore]
        public virtual OfficeAssignment OfficeAssignment { get; set; }
        [UscField(UscFieldAttrValues.ReadOnly)]
        [JsonIgnore]
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
        [UscField(UscFieldAttrValues.ReadOnly)]
        [JsonIgnore]
        public virtual ICollection<Department> Departments { get; set; }
        [UscField(UscFieldAttrValues.ReadOnly)]
        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }        
    }

    public class PersonData
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string Discriminator { get; set; }
    }
}
