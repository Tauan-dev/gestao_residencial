using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.DTOs.Category;

namespace ApiGastosResidenciais.Application.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(CreateCategoryDto categoryDto);
        Task UpdateAsync(int id, UpdateCategoryDto categoryDto);
        Task DeleteAsync(int id);
        Task<CategoryDto> GetByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
    }
}