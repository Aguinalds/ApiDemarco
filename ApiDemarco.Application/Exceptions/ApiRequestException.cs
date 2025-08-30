namespace ApiDemarco.Application.Exceptions;

public class ApiRequestException : Exception
{
    public ApiRequestException(Exception innerException)
        : base("Ocorreu um erro durante a requisição da API", innerException) { }    
}