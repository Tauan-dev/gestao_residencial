using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.DTOs.Transaction;

namespace ApiGastosResidenciais.Application.Interfaces
{
    public interface ITransactionService
    {
        Task CreateAsync(CreateTransactionDto transactionDto);
        Task UpdateAsync(int id, UpdateTransactionDto transactionDto);
        Task DeleteAsync(int id);
        Task<TransactionDto> GetByIdAsync(int id);
        Task<IEnumerable<TransactionDto>> GetAllAsync();
    }
}