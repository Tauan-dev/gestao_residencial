using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGastosResidenciais.Application.DTOs.Person
{
    public class PersonSpenDto
    {
        public int PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Expense { get; set; }
    }
}