using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Domain.Enums;
using ApiGastosResidenciais.Domain.Validation;

namespace ApiGastosResidenciais.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public CategoryPurpose Purpose { get; set; } // utilizar Enum para padronizar facilita muito a legibilidade do codigo, e evita problemas como erros ou inconsistencias de dados.
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        public Category(string description, CategoryPurpose purpose)
        {
            ValidateDomain(description, purpose);
            Transactions = new List<Transaction>();
        }

        private void ValidateDomain(string description, CategoryPurpose purpose)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(description), "Descrição é obrigatoria");
            DomainExceptionValidation.When(description.Length < 3, "Descrição muito curta");
            Description = description;
            Purpose = purpose;

        }

        public void Update(string description, CategoryPurpose purpose)
        {
            ValidateDomain(description, purpose);
        }
    }

}