using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Description { get; set; }
        public CategoryPurpose Purpose { get; set; }
        public ICollection<Transaction> Transactions { get; private set; }

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

        // public void Update(string description, CategoryPurpose purpose)
        // {
        //     ValidateDomain(description, purpose);
        // }
    }

}