using MediatR;

namespace ApiDemarco.Application.Features.Clientes.Commands;

public class CreateClienteCommand(string nome, string email) : IRequest<string>
{
    /// <summary>
    /// Gets or sets the name of the cliente. This property represents the cliente's full name,
    /// which is typically required for cliente creation or identification in the application.
    /// </summary>
    public required string Nome { get; set; } = nome;
    
    /// <summary>
    /// Gets or sets the email address of the cliente. This property represents
    /// the cliente's primary email used for communication and identification
    /// purposes within the application.
    /// </summary>
    public required string Email { get; set; } = email;
}