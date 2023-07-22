using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Lab06.Models.DataAccess;

namespace Lab7.Models.DataAccess
{
    public class AcademicValidation
    {
        public string Id { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;

        public bool valide = false;

        public List<AcademicValidation> academicValidations { get; set; }
        public AcademicValidation()
        {

        }

    }
}
