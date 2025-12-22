using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Validation;

namespace Api.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<Transaction> Transactions { get; set; }


        public Person(string name, int age)
        {

            ValidateDomain(name, age);
            Transactions = new List<Transaction>();
        }


        private void ValidateDomain(string name, int age)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome é obrigatorio");
            DomainExceptionValidation.When(age < 0 || age > 120, "Idade invalida");
            Name = name;
            Age = age;
        }

        // public void Update(string name, int age)
        // {
        //     ValidateDomain(name, age);
        // }
    }
}