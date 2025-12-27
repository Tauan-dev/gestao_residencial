# Gastos Residenciais

## Descrição

Este projeto implementa um sistema para gerenciamento de gastos residenciais, permitindo controle financeiro completo através de funcionalidades de cadastro de pessoas, categorias de despesas e transações. Desenvolvido com .NET 9, a aplicação segue os princípios de Clean Architecture, garantindo separação de responsabilidades, manutenibilidade e testabilidade.

A solução adota padrões modernos de desenvolvimento backend (.NET Core Web API), integração com banco de dados relacional (MySQL via Entity Framework Core), mapeamento objeto-relacional (AutoMapper), e habilitação de CORS para consumo por aplicações frontend SPA (React).

---

## API Backend - Gestão Residencial

[![.NET](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/)
[![MySQL](https://img.shields.io/badge/Database-MySQL-blue)](https://www.mysql.com/)
[![Build](https://img.shields.io/badge/build-passing-brightgreen)]()
[![License](https://img.shields.io/badge/license-MIT-green)]()

## Decisões de Arquitetura

- **Camadas Separadas:** O projeto segue o padrão de camadas (Domain, Application, Infra, WebApi), facilitando manutenção, testes e evolução. Cada camada tem responsabilidades claras, mesmo não separando fisicamente em projetos distintos, como seria na arquiterura limpa clássica, por exemplo. Mas da uma boa organização ao código.
- **Entity Base:** Todas as entidades herdam de `BaseEntity`, centralizando propriedades comuns como `Id`, `CreatedAt`, `UpdatedAt`, `IsDeleted` e `DeletedAt`. Isso evita repetição de código e facilita a implementação de soft delete.
- **Soft Delete:** Implementado via propriedade `IsDeleted` e método `SoftDelete()`, permitindo exclusão lógica sem perder histórico.
- **DTOs e AutoMapper:** Uso de DTOs para comunicação entre camadas e AutoMapper para conversão, promovendo desacoplamento e segurança.
- **Services Genéricos:** Serviços como `CalculationService` são genéricos e reutilizáveis, aplicando o princípio DRY (Don't Repeat Yourself) para cálculos financeiros em diferentes contextos (pessoas, categorias).
- **Validações Centralizadas:** Mensagens e regras de validação estão em `ValidationRules`, facilitando manutenção e padronização nas entidades.

## Camadas do Projeto

- **Domain:** Contém as Entities principais (Pessoa, Categoria, Transacao), Enums, Interfaces de repositórios e regras de validação.
- **Application:** Services, Services Interfaces, DTOs e mapeamentos AutoMapper.
- **Infra:** Implementações de repositórios, contexto do banco e configurações.
- **WebApi:** Camada de apresentação, controllers e configuração da API.

## Lógica de Negócio

- **Cálculos Financeiros:** Métodos genéricos para calcular totais, saldos e identificar maiores/menores gastos, utilizando LINQ para coleções e algoritmos como QuickSort para ordenação eficiente.
- **Agrupamento e Totais:** Interfaces e serviços permitem agrupar dados por proprietário (pessoa/categoria) e calcular totais de receitas, despesas e saldo.

- **Maior e Menor Gasto:** Implementação de algoritmo QuickSort para ordenar transações e identificar maiores e menores gastos de forma eficiente.

- **Endpoints REST:** Controllers expõem endpoints RESTful para CRUD de pessoas, categorias e transações, além de rotas específicas para totais e agrupamentos.

## Banco de Dados

- **MySQL:** Utilização do MySQL como banco relacional, com migrations gerenciadas pelo Entity Framework Core.
- **Migrations:** Estrutura de tabelas criada via migrations, com suporte a chaves estrangeiras, soft delete e campos de auditoria.
- **Configurações:** As entidades são configuradas via classes de configuração (`IEntityTypeConfiguration`), garantindo integridade e restrições no banco.

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

## Documentação da API

A documentação interativa da API está disponível via Swagger. Após rodar a aplicação, acesse:

```
http://localhost:5233/swagger/index.html
```

## Padrão de Commit

Este projeto segue as diretrizes do artigo sobre [Padrão de Commit](https://dev.to/renatoadorno/padroes-de-commits-commit-patterns-41co).

## Observações

- O projeto foi pensado para ser extensível e de fácil manutenção, com foco em boas práticas de desenvolvimento.

- Como a lógica entre Persoas e Categorias é similar, optei por reutilizar o máximo possível de código entre elas, fiz a parce opcional descrita no escopo do sitema.

- Como indicado, fiz comentários no código para explicar decisões pontuais.

- AutoMapper foi configurado para segurança de dados e desacoplamento entre camadas.

- CORS está habilitado para permitir consumo por frontends SPA.

- A propagação de dados é mantida entre frontend e backend via DTOs fortemente tipados, mesmo reinicializando o projeto frontend.

- Validações foram feitas nos Dtos para garantir integridade dos dados.

- Me sinto mais confortável com .NET no backend, então optei por dar mais ênfase nele dentro do projeto.

- Algumas melhorias futuras podem ser feitas, como utilizar docker para containerização, adicionar testes unitários e de integração, deploy em nuvem, porém me mantive focado em deixar a aplicação fechada no que tange ao escopo proposto.

---

## Frontend - Gestão Residencial

[![React](https://img.shields.io/badge/React-19-blue?logo=react)](https://react.dev/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.9-blue?logo=typescript)](https://www.typescriptlang.org/)
[![Vite](https://img.shields.io/badge/Vite-7.2-purple?logo=vite)](https://vitejs.dev/)
[![ESLint](https://img.shields.io/badge/ESLint-enabled-blueviolet?logo=eslint)](https://eslint.org/)
[![License: MIT](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

### Tecnologias Utilizadas

- **React 19**: Biblioteca principal para construção da interface.
- **TypeScript**: Tipagem estática para maior robustez e produtividade.
- **Vite**: Ferramenta de build e desenvolvimento rápido.
- **React Router DOM 7**: Gerenciamento de rotas SPA.
- **ESLint**: Padronização e qualidade do código.
- **Axios**: Requisições HTTP para integração com a API.
- **CSS Modules**: Estilização modular e escopada por componente.

### Estrutura de Pastas

- `src/pages`: Páginas principais (Pessoas, Categorias, Transações).
- `src/components`: Componentes reutilizáveis, organizados por domínio.
- `src/services`: Serviços para comunicação com a API.
- `src/types`: Tipos TypeScript compartilhados.
- `src/styles`: Estilos globais e utilitários.

### Principais Decisões

- **Separação por domínio**: Componentes e serviços organizados por contexto (pessoas, categorias, transações).
- **Interfaces e Tipos**: Uso extensivo de interfaces TypeScript para garantir contratos claros.
- **Componentização**: Componentes pequenos e reutilizáveis para facilitar manutenção.
- **Serviços de API**: Centralização das chamadas HTTP em serviços dedicados.
- **Tipagem forte**: Contratos claros entre frontend e backend, facilitando manutenção e evolução.
- **Estilização modular**: Uso de CSS Modules para evitar conflitos de estilos.
- **Roteamento**: Navegação fluida entre páginas sem recarregamento.

Por escolha, resolvi manter o frontend mais simples possível, focando na funcionalidade e integração com a API .NET. A interface é funcional e direta, priorizando a experiência do usuário na gestão dos dados.

### Como rodar o projeto

```bash
cd Frontend/gestao-residencial-frontend
npm install
npm run dev
```
