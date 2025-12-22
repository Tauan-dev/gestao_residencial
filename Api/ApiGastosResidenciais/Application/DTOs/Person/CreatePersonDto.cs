using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiGastosResidenciais.Application.DTOs.Validation;

namespace ApiGastosResidenciais.Application.DTOs.Person
{
    public class CreatePersonDto
    {
        [Required(ErrorMessage = ValidationRules.RequiredError)]
        [RegularExpression(ValidationRules.PersonNameRegex, ErrorMessage = ValidationRules.PersonNameError)]
        public string Name { get; set; } = string.Empty;

        [Range(ValidationRules.MinAge, ValidationRules.MaxAge, ErrorMessage = ValidationRules.AgeError)]
        public int Age { get; set; }
    }
}