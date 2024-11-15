# Gerenciador de Veículos

![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white)

![Entity Framework](https://img.shields.io/badge/Entity_Framework-7A7A7A?style=flat&logo=entity-framework&logoColor=white)

![SQLite](https://img.shields.io/badge/SQLite-003B57?style=flat&logo=sqlite&logoColor=white)

![Swagger](https://upload.wikimedia.org/wikipedia/commons/thumb/a/ab/Swagger-logo.png/64px-Swagger-logo.png)


O **Gerenciador de Veículos** é uma aplicação que permite o gerenciamento de veículos, construída com **Windows Forms** no frontend e **ASP.NET Core** no backend, utilizando **Entity Framework** para interagir com um banco de dados **SQLite**. A aplicação possibilita funcionalidades como listagem, cadastro, atualização e exclusão de veículos, além de exibir informações sobre os usuários cadastrados.

## Funcionalidades

- **Listar veículos**: Exibe todos os veículos cadastrados no sistema.
- **Visualizar veículo por ID**: Permite visualizar informações detalhadas de um veículo específico.
- **Cadastrar veículos**: Cadastro de novos veículos no sistema.
- **Atualizar informações do veículo**: Permite atualizar os dados de um veículo cadastrado.
- **Deletar veículo**: Exclusão de um veículo do sistema.
- **Gerenciamento de usuários**: Exibe todos os usuários cadastrados no sistema.
  
Além disso, a aplicação possui um sistema de autenticação com **JWT (JSON Web Tokens)** e controle de **Roles** (Administrador e Cliente). Somente o usuário com o **role** de **Administrador** tem permissão para realizar todas as operações (CRUD) no sistema, enquanto o **Cliente** tem permissão apenas para visualizar os veículos e consultar veículos específicos por ID.

## Tecnologias Utilizadas

- **Frontend**: 
  - Windows Forms
- **Backend**:
  - ASP.NET Core
  - Entity Framework Core
- **Autenticação**:
  - JWT Bearer Tokens
- **Banco de Dados**:
  - SQLite
- **Documentação**:
  - Swagger
