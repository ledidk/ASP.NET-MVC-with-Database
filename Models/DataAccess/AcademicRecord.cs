using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lab06.Models.DataAccess;

namespace Lab06.Models.DataAccess
{
    public partial class AcademicRecord
    {
        public string CourseCode { get; set; } = null!;
        public string StudentId { get; set; } = null!;
        [Required(ErrorMessage = "Must enter grade")]
        [Range(0, 100, ErrorMessage ="Must be between 0 and 100")]
        public int? Grade { get; set; }
        [Display(Name = "Course")]
        public virtual Course CourseCodeNavigation { get; set; } = null!;
        [Display(Name = "Student")]
        public virtual Student Student { get; set; } = null!;
    }
}
