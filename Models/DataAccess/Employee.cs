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
    public partial class Employee
    {
        public Employee()
        {
            Roles = new HashSet<Role>();
        }
        public int Id { get; set; }
        [Display(Name ="Employee Name")]
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z]+ [a-zA-Z]+$", ErrorMessage ="Must be in the form of firstname followed by last name")]
        public string Name { get; set; } = null!;
        [Display(Name = "Network ID")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "User name must be more than 3 characters.")]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = null!;
        [Display(Name = "Password")]
        [StringLength(30,MinimumLength = 5, ErrorMessage = "The password must be at least 5 characters.")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
        [Display(Name = "Job Title(s)")]
        [Required(ErrorMessage = "You must select at least one role!")]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
