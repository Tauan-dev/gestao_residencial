using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Domain.Enums;

namespace ApiGastosResidenciais.Application.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public CategoryPurpose Purpose { get; set; }
    }
}