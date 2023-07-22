using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Lab06.Models.DataAccess;

namespace Lab06.Models.DataAccess
{
    public partial class Course
    {
        public Course()
        {
            AcademicRecords = new HashSet<AcademicRecord>();
            StudentStudentNums = new HashSet<Student>();
        }

        public string Code { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? HoursPerWeek { get; set; }
        public decimal? FeeBase { get; set; }

        public virtual ICollection<AcademicRecord> AcademicRecords { get; set; }

        public virtual ICollection<Student> StudentStudentNums { get; set; }
    }
}
