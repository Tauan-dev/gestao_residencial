using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGastosResidenciais.Application.Interfaces
{
    public interface ICalculationService
    {
        // minha intenção é agrupar em coleções que ao utilizar os metódos de LINQ, posso fazer cálculos de modo generico e mais limpo, com melhor estrura de leitura de código e reaproveitamento.
        public IEnumerable<OwnerTotals> CalculatePerOwner(IEnumerable<CalculationInput> input);
        public CalculatedResult CalculateTotal(IEnumerable<CalculationInput> input);

        public SpentResult[] Spent(IEnumerable<CalculationInput> input);
    }

    public record CalculationInput(int Id, decimal FinanceIncome, decimal Expense);

    public record CalculatedResult(decimal TotalIncome, decimal TotalExpense, decimal Balance);

    public record OwnerTotals(int Id, decimal TotalIncome, decimal TotalExpense, decimal Balance);
    public record SpentResult(int Id, decimal Expense);


}