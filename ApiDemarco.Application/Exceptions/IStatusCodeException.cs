namespace ApiDemarco.Application.Exceptions;

public interface IStatusCodeException
{
    int StatusCode { get; }
}