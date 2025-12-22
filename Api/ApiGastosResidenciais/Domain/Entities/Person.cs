using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Domain.Validation;

namespace ApiGastosResidenciais.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();


        public Person(string name, int age)
        {

            ValidateDomain(name, age);
            Transactions = new List<Transaction>();
        }

        // aqui eu utilizo o metodo validade para garantir conscientência de dados e evitar repetição de codigo no construtor e em outros metodos. Utilizo esse metodo nas outras entidades tambem.
        private void ValidateDomain(string name, int age)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Nome é obrigatorio");
            DomainExceptionValidation.When(age < 0 || age > 120, "Idade invalida");
            Name = name;
            Age = age;
        }

        public void Update(string name, int age)
        {
            ValidateDomain(name, age);
        }
            
        public bool IsMinor()
        {
            return Age < 18;
        }
    }
}