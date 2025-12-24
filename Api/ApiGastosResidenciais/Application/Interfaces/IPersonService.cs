using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.DTOs.Person;

namespace ApiGastosResidenciais.Application.Interfaces
{
    public interface IPersonService
    {
        Task CreateAsync(CreatePersonDto personDto);
        Task UpdateAsync(int id, UpdatePersonDto personDto);
        Task DeleteAsync(int id);
        Task<(IEnumerable<PersonTotalsDto> Itens, CalculatedResult Total)> GetTotalsByPersonAsync();
        Task<SpentResult[]> GetSpentAsync();
        Task<PersonDto> GetByIdAsync(int id);
        Task<IEnumerable<PersonDto>> GetAllAsync();
    }
}