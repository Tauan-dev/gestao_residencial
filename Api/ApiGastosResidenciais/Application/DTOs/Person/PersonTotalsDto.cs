using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGastosResidenciais.Application.DTOs.Person
{
    public class PersonTotalsDto
    {
        public int PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        
    }
}