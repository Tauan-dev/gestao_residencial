using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGastosResidenciais.Application.DTOs.Category
{
    public class CategorySpenDto
    {
        public CategorySpenDto(int CategoryId, string Description, decimal Expense)
        {
            this.CategoryId = CategoryId;
            this.Description = Description;
            this.Expense = Expense;
        }

        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Expense { get; set; }
    }
}