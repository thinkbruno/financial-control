# Financial Control

Sistema de controle financeiro com foco em arquitetura limpa, desacoplamento e escalabilidade.

---

## Visão Geral

Aplicação backend para gerenciamento de transações financeiras (receitas e despesas), construída com princípios de Clean Architecture e Domain-Driven Design (DDD).

O sistema suporta processamento assíncrono via eventos, permitindo evolução para cenários distribuídos.

---

## Arquitetura

O projeto está estruturado em camadas:

- API → entrada HTTP
- Application → casos de uso
- Domain → regras de negócio
- Infrastructure → persistência e mensageria
- Worker → consumo de eventos

### Diagrama de Arquitetura

    ┌──────────────┐
    │     API      │
    └──────┬───────┘
           │
    ┌──────▼───────┐
    │ Application  │
    │ (Use Cases)  │
    └──────┬───────┘
           │
    ┌──────▼───────┐
    │   Domain     │
    │ (Entities)   │
    └──────┬───────┘
           │
    ┌──────▼───────────────┐
    │   Infrastructure     │
    │ EF Core + RabbitMQ   │
    └─────────┬────────────┘
              │
       ┌──────▼──────┐
       │   Worker    │
       │ (Consumers) │
       └─────────────┘

---

## Tecnologias

- .NET 8
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- MassTransit
- RabbitMQ
- xUnit
- Moq

---

## Fluxo de Criação de Transação

Client → API → UseCase → Domain → Repository → Database
↓
EventPublisher
↓
RabbitMQ
↓
Worker

---

## Exemplo de API

### Criar Transação

POST `/transactions`

Request:

{
"description": "Salário",
"amount": 5000,
"date": "2026-01-01T00:00:00Z",
"type": "Income",
"category": "Salário"
}

Response:

{
"id": "uuid",
"description": "Salário",
"amount": 5000,
"type": "Income"
}

---

### Listar Transações

GET `/transactions`

Response:

[
{
"id": "uuid",
"description": "Salário",
"amount": 5000,
"type": "Income"
}
]

---

## Testes

- Testes unitários com xUnit
- Mock de dependências com Moq
- Validação de comportamento (AAA)

---

## Como Executar

Build:

dotnet build

Executar API:

dotnet run --project src/FinancialControl.Api

Executar Worker:

dotnet run --project src/FinancialControl.Worker

Rodar testes:

dotnet test

---

## Configuração

Arquivo:

src/FinancialControl.Api/appsettings.json

Exemplo:

{
"ConnectionStrings": {
"DefaultConnection": "Host=localhost;Port=5432;Database=financial;Username=postgres;Password=postgres"
}
}

---

## Decisões Técnicas

- Application desacoplada de infraestrutura via interfaces
- Uso de eventos para comunicação assíncrona
- Domínio isolado e responsável pelas regras de negócio
- Testes focados em comportamento

---

## Evolução do Projeto

O sistema foi desenvolvido de forma incremental:

1. Modelo simples (CRUD)
2. Introdução de camadas
3. Aplicação de DDD
4. Adição de mensageria
5. Refatoração para desacoplamento total

---

## Autor

Bruno Ramos

Acesse meu portfólio:  
[www.brunoramos.tec.br](https://www.brunoramos.tec.br)
