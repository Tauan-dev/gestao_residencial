using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Domain.Interfaces;
using ApiGastosResidenciais.Domain.Entities;
using ApiGastosResidenciais.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiGastosResidenciais.Infra.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
                throw new InvalidOperationException($"Pessoa com {id} não foi encontrado.");
            return person;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var person = await GetByIdAsync(id);
            if (person != null)
            {
                person.SoftDelete();
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
    }

}