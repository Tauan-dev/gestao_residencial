using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Enums;

namespace Api.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public string Description { get; set; }
        public int Value { get; set; }
        public TransactionType Type { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Transaction(string description, int value, TransactionType type, int personId, int categoryId)
        {
            ValidateDomain(description, value, type, personId, categoryId);
        }

        public void ValidateDomain(string description, int value, TransactionType type, int personId, int categoryId)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(description), "Descrição é obrigatoria");
            DomainExceptionValidation.When(value < 0, "Valor não pode ser negativo");
            Description = description;
            Value = value;
            Type = type;
            PersonId = personId;
            CategoryId = categoryId;
        }

        // public void Update()
        // {
        //     ValidateDomain(Description, Value, Type, PersonId, CategoryId);
        // }
    }
}