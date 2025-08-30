using ApiDemarco.Application.Logging;
using MongoDB.Driver;

namespace ApiDemarco.Infrastructure.Logging;

public class MongoLoggerAdapter<T> : IAppLogger<T>
{
    private readonly IMongoCollection<LogAcao> _collection;

    public MongoLoggerAdapter(IMongoClient client)
    {
        var db = client.GetDatabase("logsdemarco");
        _collection = db.GetCollection<LogAcao>("logs");
    }
    
    public void LogInformation(string message, params object[] args) =>
        Save("Information", message, args);

    public void LogWarning(string message, params object[] args) =>
        Save("Warning", message, args);

    public void LogError(string message, Exception ex, params object[] args) =>
        Save("Error", $"{message} - {ex.Message}", args);

    public void LogError(Exception ex) =>
        Save("Error", ex.ToString());

    public void LogError(string message, params object[] args) =>
        Save("Error", message, args);

    private void Save(string level, string message, params object[] args)
    {
        var log = new LogAcao
        {
            DataHora = DateTime.UtcNow,
            Acao = $"{level}: {string.Format(message, args)}",
            IdCliente = 0
        };
        _collection.InsertOne(log);
    }
    
    public void LogSuccess(string acao, int idCliente)
    {
        var log = new LogAcao
        {
            DataHora = DateTime.UtcNow,
            Acao = acao,
            IdCliente = idCliente
        };
        _collection.InsertOne(log);
    }

    public class LogAcao
    {
        public DateTime DataHora { get; set; }
        public string Acao { get; set; }
        public int IdCliente { get; set; }
    }
}