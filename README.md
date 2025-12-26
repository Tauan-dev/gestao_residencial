# API Gastos Residenciais

[![.NET](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/)
[![MySQL](https://img.shields.io/badge/Database-MySQL-blue)](https://www.mysql.com/)
[![Build](https://img.shields.io/badge/build-passing-brightgreen)]()
[![License](https://img.shields.io/badge/license-MIT-green)]()

## Descrição

API para gestão de gastos residenciais, permitindo o controle de categorias, pessoas e transações financeiras. O projeto foi desenvolvido em .NET 9, utilizando arquitetura em camadas, Entity Framework Core e banco de dados MySQL.

---

## Decisões de Arquitetura

- **Camadas Separadas:** O projeto segue o padrão de camadas (Domain, Application, Infra, WebApi), facilitando manutenção, testes e evolução.
- **Entity Base:** Todas as entidades herdam de `BaseEntity`, centralizando propriedades comuns como `Id`, `CreatedAt`, `UpdatedAt`, `IsDeleted` e `DeletedAt`. Isso evita repetição de código e facilita a implementação de soft delete.
- **Soft Delete:** Implementado via propriedade `IsDeleted` e método `SoftDelete()`, permitindo exclusão lógica sem perder histórico.
- **DTOs e AutoMapper:** Uso de DTOs para comunicação entre camadas e AutoMapper para conversão, promovendo desacoplamento e segurança.
- **Services Genéricos:** Serviços como `CalculationService` são genéricos e reutilizáveis, aplicando o princípio DRY (Don't Repeat Yourself) para cálculos financeiros em diferentes contextos (pessoas, categorias).
- **Validações Centralizadas:** Mensagens e regras de validação estão em `ValidationRules`, facilitando manutenção e padronização.

---

## Lógica de Negócio

- **Cálculos Financeiros:** Métodos genéricos para calcular totais, saldos e identificar maiores/menores gastos, utilizando LINQ para coleções e algoritmos como QuickSort para ordenação eficiente.
- **Agrupamento e Totais:** Interfaces e serviços permitem agrupar dados por proprietário (pessoa/categoria) e calcular totais de receitas, despesas e saldo.
- **Endpoints REST:** Controllers expõem endpoints RESTful para CRUD de pessoas, categorias e transações, além de rotas específicas para totais e agrupamentos.

---

## Banco de Dados

- **MySQL:** Utilização do MySQL como banco relacional, com migrations gerenciadas pelo Entity Framework Core.
- **Migrations:** Estrutura de tabelas criada via migrations, com suporte a chaves estrangeiras, soft delete e campos de auditoria.
- **Configurações:** As entidades são configuradas via classes de configuração (`IEntityTypeConfiguration`), garantindo integridade e restrições no banco.

---

## Como rodar

1. Configure a string de conexão MySQL em `appsettings.json`.
2. Execute as migrations:
   ```
   dotnet ef database update
   ```
3. Rode a aplicação:
   ```
   dotnet run --project ApiGastosResidenciais
   ```
4. Acesse a documentação Swagger em `/swagger`.

---

## Principais Endpoints

- `GET /api/category` — Lista categorias
- `GET /api/person` — Lista pessoas
- `GET /api/transaction` — Lista transações
- `GET /api/category/totals` — Totais por categoria
- `GET /api/person/totals` — Totais por pessoa

---

## Observações

- O projeto foi pensado para ser extensível e de fácil manutenção.
- Comentários no código explicam decisões como uso de entidades base, serviços genéricos e agrupamento de cálculos.
- O uso de Soft Delete permite manter histórico sem perder dados.

---

## Licença

MIT
