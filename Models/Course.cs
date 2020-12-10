using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable

namespace ASPNETCore5Demo.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseInstructors = new HashSet<CourseInstructor>();
            Enrollments = new HashSet<Enrollment>();
        }

        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public void UpdateFrom(object source)
        {
            Type myClass = typeof(Course);
            foreach(var prop in source.GetType().GetProperties())
            {
                var myProp = myClass.GetProperty(prop.Name);
                if (myProp != null)
                    myProp.SetValue(this, prop.GetValue(source));

            }

        }
    }
}
