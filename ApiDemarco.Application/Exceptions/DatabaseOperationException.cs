namespace ApiDemarco.Application.Exceptions;

public class DatabaseOperationException : Exception
{
    public DatabaseOperationException(Exception ex)
        : base("Ocorreu um erro em uma operação de banco de dados.", ex) { }

    public DatabaseOperationException(string message)
        : base(string.IsNullOrWhiteSpace(message) ? "Ocorreu um erro em uma operação de banco de dados." : message) { }

    public DatabaseOperationException(string message = "", Exception? innerException = null)
        : base(string.IsNullOrWhiteSpace(message) ? "Ocorreu um erro em uma operação de banco de dados." : message, innerException) { }
}