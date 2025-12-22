using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGastosResidenciais.Application.DTOs
{
    public static class ValidationRules
    {

        // Ao criar a classe de validação para os DTO isso me permite centralizar algumas regras ou  textos de erro padrão, pensando em um contexto escalavél essas regras poderiam ser aplicadas a novas entidades. Ao inves de mudar em todos os DTOs posso alterar a validação na classe e toda a aplicação vai utilizar o padrão

        public const string RequiredError = "O campo é obrigatório.";

        public const string PersonNameRegex = @"^[\p{L}\p{M}\s']+$";
        public const string PersonNameError = "O nome deve conter apenas letras e espaços.";

        public const int MinAge = 1;
        public const int MaxAge = 120;
        public const string AgeError = "A idade deve ser um número inteiro positivo.";


        public const string CategoryDescriptionError =
        "A descrição da categoria é obrigatória.";

        public const int CategoryDescriptionMinLength = 3;
        public const int CategoryDescriptionMaxLength = 700;

        public const string CategoryPurposeError =
            "A finalidade deve ser despesa, receita ou ambas.";

        public const string TransactionDescriptionError =
            "A descrição da transação é obrigatória.";

        public const string TransactionTypeError =
            "O tipo deve ser 'despesa' ou 'receita'.";

        public const string TransactionValueError =
            "O valor deve ser um número decimal positivo.";

        public const string ForeignKeyError =
            "O identificador informado é inválido.";
    }

}