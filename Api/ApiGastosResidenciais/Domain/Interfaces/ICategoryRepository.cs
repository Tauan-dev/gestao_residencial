using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Domain.Entities;

namespace ApiGastosResidenciais.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}