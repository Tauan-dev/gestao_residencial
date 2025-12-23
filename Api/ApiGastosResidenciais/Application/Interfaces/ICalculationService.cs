using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGastosResidenciais.Application.Interfaces
{
    public interface ICalculationService
    {
        CalculatedResult Calculate(IEnumerable<CalculationInput> input);

        public SpentResult[] Spent(IEnumerable<CalculationInput> input);
    }

    public record CalculationInput(int Id, decimal FinanceIncome, decimal Expense);

    public record CalculatedResult(decimal TotalIncome, decimal totalExpense, decimal Balance);

    public record SpentResult(int Id, decimal Expense);


}