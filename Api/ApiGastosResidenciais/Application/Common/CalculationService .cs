using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.Interfaces;
using ApiGastosResidenciais.Application.Common;
using ApiGastosResidenciais.Domain.Interfaces;
using System.Threading.RateLimiting;

namespace ApiGastosResidenciais.Application.Common
{
    public class CalculationService : ICalculationService
    {
        // Como os métodos criados são utilizados de forma 'padrão' entre o service de Pessoas e o de Categorias(mesmo que opcional), pra mim foi interessante criar um service genérico comum para ambos, respeitando o princípio do DRY (Don't Repeat Yourself). Minha intenção é poder fazer o sistema de forma que evite repetições descessárias.
        public IEnumerable<OwnerTotals> CalculatePerOwner(IEnumerable<CalculationInput> input)
        {
            return input
                .GroupBy(i => i.Id)
                .Select(g =>
            {
                decimal totalIncome = g.Sum(i => i.FinanceIncome);
                decimal totalOutcome = g.Sum(i => i.Expense);

                return new OwnerTotals(
                    Id: g.Key,
                    TotalIncome: totalIncome,
                    TotalExpense: totalOutcome,
                    Balance: totalIncome - totalOutcome);
            });
        }

        public CalculatedResult CalculateTotal(IEnumerable<CalculationInput> input)
        {
            decimal totalIncome = input.Sum(i => i.FinanceIncome);
            decimal totalExpense = input.Sum(i => i.Expense);
            decimal balance = totalIncome - totalExpense;

            return new CalculatedResult(totalIncome, totalExpense, balance);
        }
        public SpentResult[] Spent(IEnumerable<CalculationInput> input)
        {
            var perOwner = CalculatePerOwner(input).ToArray();
            if (perOwner.Length == 0)
            {
                return Array.Empty<SpentResult>();
            }
            var array = perOwner
                .Select(o => new CalculationInput(
                    Id: o.Id,
                    FinanceIncome: 0m,
                    Expense: o.TotalExpense
                ))
                .ToArray();
                
            QuickSort(array, 0, array.Length - 1);

            var min = array[0];
            var max = array[array.Length - 1];

            return new[]
            {
                new SpentResult(min.Id, min.Expense),
                new SpentResult(max.Id, max.Expense)
            };
        }

        public void QuickSort(CalculationInput[] array, int left, int rigth)
        {
            if (left < rigth)
            {
                int pivo = Partition(array, left, rigth);
                QuickSort(array, left, pivo - 1);
                QuickSort(array, pivo + 1, rigth);
            }
        }

        public int Partition(CalculationInput[] array, int left, int rigth)
        {
            decimal pivo = array[rigth].Expense;
            int i = left - 1;
            for (int j = left; j < rigth; j++)
            {
                if (array[j].Expense < pivo)
                {
                    i++;
                    (array[i], array[j]) = (array[j], array[i]);
                }
            }
            (array[i + 1], array[rigth]) = (array[rigth], array[i + 1]);
            return i + 1;
        }


    }
}