using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;    
using ApiGastosResidenciais.Application.DTOs.Validation;
using ApiGastosResidenciais.Domain.Enums;

namespace ApiGastosResidenciais.Application.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = ValidationRules.RequiredError)]
        [StringLength(ValidationRules.CategoryDescriptionMaxLength, MinimumLength = ValidationRules.CategoryDescriptionMinLength, ErrorMessage = ValidationRules.CategoryDescriptionError)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationRules.CategoryPurposeRequiredError)]
        public CategoryPurpose Purpose { get; set; }
    }
}