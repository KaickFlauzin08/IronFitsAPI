# 🏋️‍♂️ IronFits API

API RESTful desenvolvida em **.NET 8** com **Entity Framework Core** para gestão de agendamentos de treinos, personal trainers, alunos e autenticação segura com JWT e encriptação PKCS/CMS.

---

## 🛠️ Tecnologias Utilizadas

* **C# / .NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core** (SQL Server)
* **JWT (JSON Web Tokens)** para autenticação e autorização
* **System.Security.Cryptography.Pkcs** para encriptação assimétrica de dados sensíveis (CPF)
* **Swagger / OpenAPI** para documentação da API

---

## 🔐 Segurança e Funcionalidades

* **Autenticação JWT:** Proteção de endpoints através de autorização por cargos (Roles = Admin).
* **Proteção de Dados (CPF):** Os CPFs dos alunos e personal trainers são encriptados no banco de dados com EnvelopedCms (PKCS #7) e descodificados apenas no retorno das consultas (GET).
* **CRUDs Diretos:**
* **Login & Utilizadores:** Registo e autenticação com geração de Token JWT.
* **Alunos:** Gestão completa (Criar, Listar, Atualizar, Eliminar).
* **Personal Trainers:** Gestão completa (Criar, Listar, Atualizar, Eliminar).
* **Agendamentos:** Associação entre Alunos e Personais com controlo de datas e estados.



---

## 🚀 Como Executar o Projeto

1. **Clonar o repositório:**
```bash
git clone https://github.com/seu-usuario/IronFitsAPI.git
cd IronFitsAPI

```


2. **Restaurar as dependências:**
```bash
dotnet restore

```


3. **Atualizar a Base de Dados (Entity Framework):**
```bash
dotnet ef database update

```


4. **Executar a aplicação:**
```bash
dotnet run

```


5. **Aceder ao Swagger:**
Abre o navegador em `https://localhost:7001/swagger` para testar os endpoints interativamente.

---

## 📋 Endpoints Principais

### 🔑 Autenticação (`/api/Login`)

| Método | Rota | Descrição |
| --- | --- | --- |
| `POST` | `/api/Login/Register` | Regista um novo utilizador |
| `POST` | `/api/Login/Login` | Realiza o login e devolve o Token JWT |
| `GET` | `/api/Login` | Lista os utilizadores registados |

---

### 👤 Alunos (`/api/CadastroAluno`)

*(Requer Token JWT com Role Admin)*

| Método | Rota | Descrição |
| --- | --- | --- |
| `GET` | `/api/CadastroAluno` | Lista todos os alunos (CPF descodificado) |
| `POST` | `/api/CadastroAluno` | Regista um aluno (CPF encriptado automaticamente) |
| `PUT` | `/api/CadastroAluno/{id}` | Atualiza os dados do aluno |
| `DELETE` | `/api/CadastroAluno/{id}` | Elimina o aluno |

---

### 🏋️‍♂️ Personal Trainers (`/api/CadastroPersonal`)

| Método | Rota | Descrição |
| --- | --- | --- |
| `GET` | `/api/CadastroPersonal` | Lista todos os personais (CPF descodificado) |
| `POST` | `/api/CadastroPersonal` | Regista um personal (CPF encriptado automaticamente) |
| `PUT` | `/api/CadastroPersonal/{id}` | Atualiza os dados do personal |
| `DELETE` | `/api/CadastroPersonal/{id}` | Elimina o personal |

---

### 📅 Agendamentos (`/api/Agendamento`)

*(Requer Token JWT com Role Admin)*

| Método | Rota | Descrição |
| --- | --- | --- |
| `GET` | `/api/Agendamento` | Lista todos os agendamentos |
| `POST` | `/api/Agendamento` | Cria um novo agendamento informando AlunoId e PersonalId |
| `PUT` | `/api/Agendamento/{id}` | Atualiza um agendamento |
| `DELETE` | `/api/Agendamento/{id}` | Elimina um agendamento |
