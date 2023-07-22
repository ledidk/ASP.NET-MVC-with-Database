using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Lab06.Models.DataAccess;

namespace Lab06.Models.DataAccess
{
    public partial class Student
    {
        public Student()
        {
            AcademicRecords = new HashSet<AcademicRecord>();
            CourseCourses = new HashSet<Course>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<AcademicRecord> AcademicRecords { get; set; }

        public virtual ICollection<Course> CourseCourses { get; set; }
    }
}
