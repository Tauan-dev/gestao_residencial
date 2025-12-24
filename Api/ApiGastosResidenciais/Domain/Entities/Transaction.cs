using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Domain.Enums;
using ApiGastosResidenciais.Domain.Validation;

namespace ApiGastosResidenciais.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public TransactionType Type { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public Transaction(string description, decimal value, TransactionType type, int personId, int categoryId)
        {
            ValidateDomain(description, value, type, personId, categoryId);
        }

        public void ValidateDomain(string description, decimal value, TransactionType type, int personId, int categoryId)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(description), "Descrição é obrigatoria");
            DomainExceptionValidation.When(value <= 0, "Valor da transação deve ser positivo");

            if (Person.IsMinor() && type == TransactionType.Receita)
            {
                DomainExceptionValidation.When(true, "Menores de idade não podem ter transações do tipo receita");
            }

            Description = description;
            Value = value;
            Type = type;
            PersonId = personId;
            CategoryId = categoryId;
        }

        public void Update(string description, decimal value, TransactionType type, int personId, int categoryId)
        {
            ValidateDomain(description, value, type, personId, categoryId);
        }
    }
}