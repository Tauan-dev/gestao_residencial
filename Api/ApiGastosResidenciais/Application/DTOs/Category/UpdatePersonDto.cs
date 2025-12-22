using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGastosResidenciais.Application.DTOs.Category
{
    public class UpdatePersonDto
    {
        [Required(ErrorMessage = ValidationRules.RequiredError)]
        [StringLength(ValidationRules.CategoryDescriptionMaxLength, MinimumLength = ValidationRules.CategoryDescriptionMinLength, ErrorMessage = ValidationRules.CategoryDescriptionError)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ValidationRules.CategoryPurposeRequiredError)]
        public CategoryPurpose Purpose { get; set; }
    }
}