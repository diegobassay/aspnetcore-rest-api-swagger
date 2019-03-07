# aspnetcore-rest-api-swagger
Projeto CRUD usando ASP.Net Core, Angular 7, Clarity, LiteDB e Swagger

## Pré-requisitos

* ASP.Net Core >= 2.2

## Preparando o projeto
Para construir o projeto executar o seguinte comando na raiz do projeto:
```
dotnet build
```
## Para executar o projeto
Depois de executar o passo acima executar o seguinte comando na raiz do projeto:
```
dotnet run
```
## Para testar a API
Foi incorporado swagger para testar requisições para a API, para acessar o Swagger digite o seguinte endereço:
```
http://localhost:5000/swagger
```
## Para Executar a interface CRUD
Acessar a url abaixo ou raiz:
```
http://localhost:5000/employee
```
## Serviços Contatos API 

|Metodo|URL|Descrição|
|------|---|-----------|
|GET|/api/employee/|Lista todos os funcionários|
|GET|/api/employee/{Id}|Obtém um item funcionário pela chave primária|
|POST|/api/employee/|Grava um novo registro de funcionário|
|PUT|/api/employee/{Id}|Modifica os informações do funcionário|
|DELETE|/api/employee/{Id}|Remove um registro de funcionário|