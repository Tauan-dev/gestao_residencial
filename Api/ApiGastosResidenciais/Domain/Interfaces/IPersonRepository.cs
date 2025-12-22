using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Domain.Entities;

namespace ApiGastosResidenciais.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task CreateAsync(Person person);
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task UpdateAsync(Person person);
        Task DeleteAsync(int id);

    }
}