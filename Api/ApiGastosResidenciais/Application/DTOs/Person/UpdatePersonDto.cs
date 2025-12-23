using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static ApiGastosResidenciais.Application.DTOs.ValidationRules;

namespace ApiGastosResidenciais.Application.DTOs.Person
{
    public class UpdatePersonDto
    {
        [Required(ErrorMessage = RequiredError)]
        [RegularExpression(PersonNameRegex, ErrorMessage = PersonNameError)]
        public string Name { get; set; } = string.Empty;

        [Range(MinAge, MaxAge, ErrorMessage = AgeError)]
        public int Age { get; set; }
    }
}