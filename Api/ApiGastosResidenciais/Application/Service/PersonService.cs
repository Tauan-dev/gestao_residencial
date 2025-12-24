using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.DTOs.Person;
using ApiGastosResidenciais.Application.Interfaces;
using ApiGastosResidenciais.Domain.Entities;
using ApiGastosResidenciais.Domain.Enums;
using ApiGastosResidenciais.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.Connections;

namespace ApiGastosResidenciais.Application.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _persons;
        private readonly IMapper _mapper;
        private readonly ICalculationService _calculation;
        private readonly ITransactionRepository _transactions;

        public PersonService(IPersonRepository persons, IMapper mapper, ICalculationService calculation, ITransactionRepository transactions)
        {
            _persons = persons;
            _mapper = mapper;
            _calculation = calculation;
            _transactions = transactions;
        }
        public async Task CreateAsync(CreatePersonDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            await _persons.CreateAsync(person);
        }

        public async Task DeleteAsync(int id)
        {
            var person = await _persons.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pessoa não encontrada");
            person.SoftDelete();
            await _persons.DeleteAsync(id);
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            var list = await _persons.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonDto>>(list);
        }

        public async Task<PersonDto> GetByIdAsync(int id)
        {
            var person = await _persons.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pessoa não encontrada");
            return _mapper.Map<PersonDto>(person);
        }



        public async Task<SpentResult[]> GetSpentAsync()
        {
            var transactions = await _transactions.GetAllAsync();

            var list = transactions.Select(t => new CalculationInput(
                Id: t.PersonId,
                FinanceIncome: t.Type == TransactionType.Receita ? t.Value : 0m,
                Expense: t.Type == TransactionType.Despesa ? t.Value : 0m));
            var spent = _calculation.Spent(list);
            return spent;
        }

        public async Task<(IEnumerable<PersonTotalsDto> Itens, CalculatedResult Total)> GetTotalsByPersonAsync()
        {
            var persons = await _persons.GetAllAsync();
            var transactions = await _transactions.GetAllAsync();

            var list = transactions.Select(t => new CalculationInput(
                Id: t.PersonId,
                FinanceIncome: t.Type == TransactionType.Receita ? t.Value : 0m,
                Expense: t.Type == TransactionType.Despesa ? t.Value : 0m));

            var perOwner = _calculation.CalculatePerOwner(list).ToList();
            var total = _calculation.CalculateTotal(list);

            var itens =
                from o in perOwner
                join p in persons on o.Id equals p.Id
                select new PersonTotalsDto
                {
                    PersonId = p.Id,
                    Name = p.Name,
                    TotalIncome = o.TotalIncome,
                    TotalExpense = o.TotalExpense
                };
            return (itens.ToList(), total);
        }

        public Task UpdateAsync(int id, UpdatePersonDto personDto)
        {
            var person = _persons.GetByIdAsync(id).Result
                ?? throw new KeyNotFoundException("Pessoa não encontrada");
            person.Update(personDto.Name, personDto.Age);
            return _persons.UpdateAsync(person);
        }
    }
}