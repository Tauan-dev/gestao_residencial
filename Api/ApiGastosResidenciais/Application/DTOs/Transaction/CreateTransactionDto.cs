using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using static ApiGastosResidenciais.Application.DTOs.ValidationRules;

namespace ApiGastosResidenciais.Application.DTOs.Transaction
{
    public class CreateTransactionDto
    {
        [Required(ErrorMessage = RequiredError)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = TransactionDescriptionError)]
        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = RequiredError)]
        [Range(0.01, double.MaxValue, ErrorMessage = TransactionValueError)]
        public decimal Value { get; set; }


        [Required(ErrorMessage = RequiredError)]
        public TransactionType Type { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ForeignKeyError)] public int PersonId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = ForeignKeyError)] public int CategoryId { get; set; }
    }
}