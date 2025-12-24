using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGastosResidenciais.Application.DTOs.Category
{
    public class CategoryTotalsDto
    {
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;
    }
}