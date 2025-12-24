using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.DTOs.Category;
using ApiGastosResidenciais.Application.Interfaces;
using ApiGastosResidenciais.Domain.Entities;
using ApiGastosResidenciais.Domain.Enums;
using ApiGastosResidenciais.Domain.Interfaces;
using AutoMapper;

namespace ApiGastosResidenciais.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categories;
        private readonly ITransactionRepository _transactions;
        private readonly ICalculationService _calculation;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categories,
            ITransactionRepository transactions,
            ICalculationService calculation,
            IMapper mapper)
        {
            _categories = categories;
            _transactions = transactions;
            _calculation = calculation;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categories.CreateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categories.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoria não encontrada");
            category.SoftDelete();
            await _categories.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var list = await _categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(list);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categories.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoria não encontrada");

            return _mapper.Map<CategoryDto>(category);
        }


        public async Task<SpentResult[]> GetSpentAsync()
        {
            var transactions = await _transactions.GetAllAsync();

            var inputs = transactions.Select(t => new CalculationInput(
                Id: t.CategoryId,
                FinanceIncome: t.Type == TransactionType.Receita ? t.Value : 0m,
                Expense: t.Type == TransactionType.Despesa ? t.Value : 0m));

            var spent = _calculation.Spent(inputs);
            return spent;
        }

        public async Task<(IEnumerable<CategoryTotalsDto> categories, CalculatedResult Total)> GetTotalsByCategoryAsync()
        {
            var categoriesList = await _categories.GetAllAsync();
            var transactions = await _transactions.GetAllAsync();

            var inputs = transactions.Select(t => new CalculationInput(
                Id: t.CategoryId,
                FinanceIncome: t.Type == TransactionType.Receita ? t.Value : 0m,
                Expense: t.Type == TransactionType.Despesa ? t.Value : 0m));

            var perOwner = _calculation.CalculatePerOwner(inputs).ToList();
            var total = _calculation.CalculateTotal(inputs);

            var items =
                from o in perOwner
                join c in categoriesList on o.Id equals c.Id
                select new CategoryTotalsDto
                {
                    CategoryId = c.Id,
                    Description = c.Description,
                    TotalIncome = o.TotalIncome,
                    TotalExpense = o.TotalExpense
                };

            return (items.ToList(), total);
        }

        public async Task UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            var category = await _categories.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Categoria não encontrada");

            category.Update(categoryDto.Description, categoryDto.Purpose);

            await _categories.UpdateAsync(category);
        }
    }
}
