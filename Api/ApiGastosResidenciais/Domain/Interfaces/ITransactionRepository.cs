using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Domain.Entities;

namespace ApiGastosResidenciais.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task CreateAsync(Transaction transaction);
        Task<Transaction> GetByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(int id);
    }
}