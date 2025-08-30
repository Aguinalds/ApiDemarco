# ApiDemarco

API pronta para uso, com todos os serviços configurados via Docker.  
Não é necessário criar arquivos `.env` ou fazer configurações adicionais — tudo já está no repositório.

---

## Pré-requisitos

- [Docker](https://www.docker.com/get-started) instalado na sua máquina
- [Docker Compose](https://docs.docker.com/compose/install/) instalado
- [.NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (somente se for rodar testes)

---

## Como rodar a aplicação

1. **Clone o repositório**

```bash
git clone https://github.com/Aguinalds/ApiDemarco.git
```
Acesse o diretório que foi feito o clone 

2. **Execute o Docker Compose**
```bash
docker-compose up -d
```

3. **Verifique se os containers estão rodando**
```bash
docker ps
```

4. **Acesse a API**
```bash
http://localhost:5000/swagger
```
## Estrutura do projeto

- ApiDemarco.Api – Projeto principal da API

- ApiDemarco.Application – Lógica de aplicação e serviços

- ApiDemarco.Domain – Entidades e regras de negócio

- ApiDemarco.Infrastructure – Persistência e integrações externas

- ApiDemarco.Tests – Projeto de testes automatizados

## Testes

O projeto possui testes automatizados para garantir o funcionamento correto da API.

## Como rodar os testes

1. **Entre na pasta de testes**
```bash
cd ApiDemarco.Test
```

2. **Execute os testes usando o .NET CLI**
```bash
dotnet test
```

## Observações

Todos os serviços, incluindo banco de dados e o MongoDb, já estão configurados no docker-compose.yml.

Não é necessário criar ou configurar variáveis de ambiente.

## Para parar a aplicação
```bash
docker-compose down
```

