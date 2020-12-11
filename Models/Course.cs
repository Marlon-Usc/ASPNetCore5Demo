using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

#nullable disable

namespace ASPNETCore5Demo.Models
{
    public partial class Course : UscEntityModelBase
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

        public DateTime DateModified { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }

    [ModelMetadataType(typeof(CourseDataMetadata))]
    public class CourseData
    {        
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
    }
   
    internal class CourseDataMetadata
    {
        [Required]
        [StringLength(30, ErrorMessage = "Title 太長")]
        public string Title { get; set; }

        [Required]
        public int Credits { get; set; }
    }
}
