namespace ApiDemarco.Application.Logging;

public interface IAppLogger<T>
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(string message, Exception ex, params object[] args);
    void LogError(Exception ex);
    void LogError(string message, params object[] args);
    void LogSuccess(string acao, int idCliente);
}