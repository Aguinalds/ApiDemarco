# ApiDemarco

API pronta para uso, com todos os serviços configurados via Docker.  
Não é necessário criar arquivos `.env` ou fazer configurações adicionais — tudo já está no repositório.

---

## Pré-requisitos

- [Docker](https://www.docker.com/get-started) instalado na sua máquina
- [Docker Compose](https://docs.docker.com/compose/install/) instalado

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
