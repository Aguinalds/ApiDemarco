using System.Net;

namespace ApiDemarco.Application.Exceptions;

public abstract class ClientesException
{
    public class EmailExistErrorException() : Exception("Já existe esse e-mail cadastrado."), IStatusCodeException
    {
        public int StatusCode => (int)HttpStatusCode.Conflict;
    }
    
    public class ClienteCreateErrorException(string nome) : Exception($"Erro ao criar o cliente {nome}."), IStatusCodeException
    {
        public int StatusCode => (int)HttpStatusCode.InternalServerError;
    }
    
    public class ClienteUpdateErrorException(string nome) : Exception($"Erro ao atualizar o cliente {nome}."), IStatusCodeException
    {
        public int StatusCode => (int)HttpStatusCode.InternalServerError;
    }
    
    public class ClienteDeleteErrorException() : Exception($"Erro ao deletar o cliente."), IStatusCodeException
    {
        public int StatusCode => (int)HttpStatusCode.InternalServerError;
    }
    
    public class ClienteNotFoundException() : Exception($"Cliente não encontrado."), IStatusCodeException
    {
        public int StatusCode => (int)HttpStatusCode.NotFound;
    }
}