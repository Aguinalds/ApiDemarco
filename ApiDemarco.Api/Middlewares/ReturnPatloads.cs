namespace ApiDemarco.Api.Middlewares;

public class Status400Payload
{
    public Status400Payload(string mensagem)
    {
        Mensagem = mensagem;
    }

    /// <summary>
    ///     Mensagem descritiva do problema.
    /// </summary>
    /// <example>Exceção de validação, onde um campo obrigatório está ausente ou inválido.</example>
    public string Mensagem { get; }
}

public class Status401Payload
{
    public Status401Payload(string mensagem)
    {
        Mensagem = mensagem;
    }

    /// <summary>
    ///     Mensagem descritiva do problema.
    /// </summary>
    /// <example>Falha na autenticação devido a token inválido ou ausente.</example>
    public string Mensagem { get; }
}

public class Status403Payload
{
    public Status403Payload(string mensagem)
    {
        Mensagem = mensagem;
    }

    /// <summary>
    ///     Mensagem descritiva do problema.
    /// </summary>
    /// <example>Você não tem permissão para acessar este recurso.</example>
    public string Mensagem { get; }
}

public class Status404Payload
{
    public Status404Payload(string mensagem)
    {
        Mensagem = mensagem;
    }

    /// <summary>
    ///     Mensagem descritiva do problema.
    /// </summary>
    /// <example>Endereço ou registro não encontrado.</example>
    public string Mensagem { get; }
}

public class Status409Payload
{
    public Status409Payload(string mensagem)
    {
        Mensagem = mensagem;
    }

    /// <summary>
    ///     Mensagem descritiva do problema.
    /// </summary>
    /// <example>A solicitação não pôde ser concluída devido a um conflito com o estado atual do recurso.</example>
    public string Mensagem { get; }
}

public class Status500Payload
{
    public Status500Payload(string mensagem)
    {
        Mensagem = mensagem;
    }

    /// <summary>
    ///     Mensagem descritiva do problema.
    /// </summary>
    /// <example>Ocorreu um erro inesperado.</example>
    public string Mensagem { get; }
}