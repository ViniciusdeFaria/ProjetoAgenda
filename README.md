[README (1).md](https://github.com/user-attachments/files/22732841/README.1.md)
# 🗓️ Projeto Agenda

Projeto criado com o objetivo de **aprimorar habilidades em C#**, aplicando conceitos de **Domain-Driven Design (DDD)** e **arquitetura em camadas**.

---

## 📘 Visão Geral

O **Projeto Agenda** é um sistema voltado para o **agendamento de serviços**, onde usuários podem se cadastrar, criar endereços, disponibilizar serviços e permitir que outros usuários realizem agendamentos.

Este projeto tem foco em aprendizado e boas práticas de modelagem de domínio e separação de responsabilidades entre camadas.

---

## ⚙️ Estado Atual

Atualmente, o repositório contém:
- A **solução principal** (`ProjetoAgenda.sln`);
- A **camada de domínio (`ProjetoAgenda.Dominio`)**, com as primeiras entidades e estrutura base;
- Início da modelagem conforme os **requisitos do domínio Agenda**.

Ainda não há camadas de aplicação, infraestrutura ou API implementadas — o foco atual está em definir corretamente o **domínio e suas regras de negócio**.

---

## 🧩 Próximos Passos

Com base no documento de requisitos, os próximos passos do projeto serão:

### 1. Domínio
- Finalizar entidades principais: **Usuário**, **Endereço**, **Serviço**, **Disponibilidade**, **Agendamento**;
- Implementar as regras de negócio descritas:
  - Criação e ativação/desativação de usuários e endereços;
  - Criação de serviços e definição de disponibilidade;
  - Realização e aceitação de agendamentos;
- Adicionar validações (ex: e-mail, senha, horários válidos).

### 2. Aplicação
- Criar casos de uso (ex: `CriarUsuario`, `CriarServico`, `AgendarServico`);
- Implementar serviços de aplicação e DTOs.

### 3. Infraestrutura
- Adicionar persistência com **Entity Framework Core**;
- Implementar repositórios e contexto de banco de dados.

### 4. API
- Criar endpoints para acesso às funcionalidades;
- Documentar via **Swagger**.

### 5. Testes
- Implementar testes de unidade para o domínio;
- Garantir que as regras de negócio estejam sendo validadas corretamente.

---

## 🧰 Tecnologias (atuais e futuras)

- **C# / .NET 8**  
- **Entity Framework Core** (planejado)  
- **xUnit / Moq** (testes)  
- **FluentValidation** (validações)  
- **ASP.NET Core Web API** (camada de apresentação)

---

## 👨‍💻 Autor

**Vinícius de Faria**  
Projeto pessoal para estudo de **C#**, **DDD** e **Arquitetura em Camadas**.
