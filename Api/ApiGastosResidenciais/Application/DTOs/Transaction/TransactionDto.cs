using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Domain.Enums;
using ApiGastosResidenciais.Domain.Entities;
using ApiGastosResidenciais.Application.DTOs.Person;
using ApiGastosResidenciais.Application.DTOs.Category;

namespace ApiGastosResidenciais.Application.DTOs.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public decimal Value { get; set; }

        public TransactionType Type { get; set; }
        public int PersonId { get; set; }
        public PersonDto? Person { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto? Category { get; set; }

    }
}