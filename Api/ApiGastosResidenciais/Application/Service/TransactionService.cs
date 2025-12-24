using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.DTOs.Transaction;
using ApiGastosResidenciais.Application.Interfaces;
using ApiGastosResidenciais.Domain.Entities;
using ApiGastosResidenciais.Domain.Enums;
using ApiGastosResidenciais.Domain.Interfaces;
using AutoMapper;

namespace ApiGastosResidenciais.Application.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _category;
        private readonly ITransactionRepository _transaction;
        private readonly IPersonRepository _person;

        public TransactionService(IMapper mapper, ICategoryRepository category, ITransactionRepository transaction, IPersonRepository person)
        {
            _mapper = mapper;
            _category = category;
            _transaction = transaction;
            _person = person;

        }

        public async Task CreateAsync(CreateTransactionDto transactionDto)
        {
            var person = await _person.GetByIdAsync(transactionDto.PersonId) ?? throw new KeyNotFoundException("Pessoa não encontrada");
            if (person != null)
            {
                ValidateAge(person, transactionDto.Type);
            }

            var category = await _category.GetByIdAsync(transactionDto.CategoryId) ?? throw new KeyNotFoundException("Categoria não encontrada");
            if (category != null)
            {
                ValidateCompatibility(category, transactionDto.Type);
            }

            var transaction = _mapper.Map<Transaction>(transactionDto);
            await _transaction.CreateAsync(transaction);
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await _transaction.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Transação não encontrada");
            transaction.SoftDelete();
            await _transaction.DeleteAsync(id);
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            var list = await _transaction.GetAllAsync();
            return _mapper.Map<IEnumerable<TransactionDto>>(list);
        }

        public async Task<TransactionDto> GetByIdAsync(int id)
        {
            var transaction = await _transaction.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Transação não encontrada");
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task UpdateAsync(int id, UpdateTransactionDto transactionDto)
        {
            var transaction = await _transaction.GetByIdAsync(id)
                 ?? throw new KeyNotFoundException("Transação não encontrada");
            var person = await _person.GetByIdAsync(transactionDto.PersonId) ?? throw new KeyNotFoundException("Pessoa não encontrada");
            if (person != null && person.Id == transactionDto.PersonId)
            {
                ValidateAge(person, transactionDto.Type);
            }

            var category = await _category.GetByIdAsync(transactionDto.CategoryId) ?? throw new KeyNotFoundException("Categoria não encontrada");
            if (category != null && category.Id == transactionDto.CategoryId)
            {
                ValidateCompatibility(category, transactionDto.Type);
            }

            transaction.Update(
                transactionDto.Description,
                transactionDto.Value,
                transactionDto.Type,
                transactionDto.PersonId,
                transactionDto.CategoryId
            );
            await _transaction.UpdateAsync(transaction);
        }


        // para evitar erros no sistema, algumas validações são importantes e "repetitivas" para metodos que agem como modificadores(create, update), centralizar elas em metodos auxilia evitar uma quantidade densa de código igual, por isso preferi separar nesses metodos 
        private void ValidateAge(Person person, TransactionType type)
        {
            if (person.IsMinor() && type == TransactionType.Receita)
            {
                throw new InvalidOperationException("Menores de idade só podem ter transações de despesas.");
            }
        }

        private void ValidateCompatibility(Category category, TransactionType type)
        {
            if (!CompatibilityCategoryTransaction(category.Purpose, type))
            {
                throw new InvalidOperationException("Transações não compatíveis com a Categoria");
            }
        }

        private static bool CompatibilityCategoryTransaction(
             CategoryPurpose purpose, TransactionType type
        )
        {
            return purpose switch
            {
                CategoryPurpose.Despesa => type == TransactionType.Despesa,
                CategoryPurpose.Receita => type == TransactionType.Receita,
                CategoryPurpose.Ambas => type == TransactionType.Despesa || type == TransactionType.Receita,
                _ => false
            };
        }
    }
}