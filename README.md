# Financial Control

Sistema de controle financeiro desenvolvido com foco em arquitetura limpa, desacoplamento e escalabilidade.

---

## Visão Geral

O projeto foi construído utilizando os princípios de:

- Clean Architecture
- Domain-Driven Design (DDD)
- SOLID
- Event-Driven Architecture

O objetivo principal deste projeto é estudar e aplicar conceitos modernos de arquitetura backend utilizando .NET 8.

Além da API, o projeto também possui uma interface web em React para gerenciamento das transações financeiras.

---

## Arquitetura

Atualmente o sistema utiliza o padrão de **Monólito Modular (Modular Monolith)**.

A escolha dessa arquitetura foi feita propositalmente para permitir:

- entendimento profundo do domínio;
- separação clara de responsabilidades;
- baixo acoplamento entre camadas;
- facilidade de manutenção;
- preparação futura para microserviços.

Mesmo sendo um monólito, o sistema já foi estruturado de forma desacoplada, permitindo evolução gradual para uma arquitetura distribuída futuramente.

### Estrutura do Projeto

```text
FinancialControl.Api
FinancialControl.Application
FinancialControl.Domain
FinancialControl.Infrastructure
FinancialControl.Worker
FinancialControl.Frontend
```

### Responsabilidades

| Projeto                         | Responsabilidade                      |
| ------------------------------- | ------------------------------------- |
| FinancialControl.Api            | Exposição das rotas HTTP              |
| FinancialControl.Application    | Casos de uso da aplicação             |
| FinancialControl.Domain         | Regras de negócio e entidades         |
| FinancialControl.Infrastructure | Banco de dados e integrações externas |
| FinancialControl.Worker         | Processamento assíncrono de eventos   |
| FinancialControl.Frontend       | Interface web React                   |

---

## Tecnologias Utilizadas

### Backend

- .NET 8
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- RabbitMQ
- MassTransit
- xUnit
- Moq

### Frontend

- React
- TypeScript
- Vite
- Axios
- TailwindCSS

### DevOps

- Docker
- Docker Compose

---

## Funcionalidades

- Criar transações
- Listar transações
- Atualizar transações
- Remover transações
- Processamento assíncrono com eventos
- Testes unitários
- API documentada com Swagger

---

## Interface Web

### Dashboard funcionando

![Dashboard](./docs/dashboard.png)

## Como Executar

### Subir containers

```bash
docker compose up --build
```

### Backend

API:

```text
http://localhost:8080
```

Swagger:

```text
http://localhost:8080/swagger
```

### Frontend

```text
http://localhost:5173
```

---

## Executar Migrations

```bash
dotnet ef database update \
  --project src/FinancialControl.Infrastructure \
  --startup-project src/FinancialControl.Api
```

---

## Executar Testes

```bash
dotnet test
```

---

## Objetivo do Projeto

Este projeto está sendo utilizado como laboratório prático para aprofundamento em:

- arquitetura de software;
- design de domínio;
- comunicação assíncrona;
- testes automatizados;
- conteinerização;
- organização de sistemas escaláveis.

A ideia é evoluir gradualmente a aplicação, mantendo uma base sólida e preparada para crescimento.

---

## Próximos Passos

- autenticação JWT;
- dashboard com gráficos;
- filtros e paginação;
- CI/CD;
- observabilidade;
- deploy em cloud;
- possível separação futura em microserviços.

---

## Autor

Bruno Ramos

Portfolio:

[www.brunoramos.tec.br](http://www.brunoramos.tec.br)
