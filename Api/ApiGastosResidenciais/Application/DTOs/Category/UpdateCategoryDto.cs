using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiGastosResidenciais.Application.DTOs;
using ApiGastosResidenciais.Domain.Enums;
using static ApiGastosResidenciais.Application.DTOs.ValidationRules;


namespace ApiGastosResidenciais.Application.DTOs.Category
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = RequiredError)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = CategoryDescriptionError)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = CategoryPurposeError)]
        public CategoryPurpose Purpose { get; set; }
    }
}