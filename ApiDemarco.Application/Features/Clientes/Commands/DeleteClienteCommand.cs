using MediatR;

namespace ApiDemarco.Application.Features.Clientes.Commands;

public class DeleteClienteCommand(int id)  : IRequest<string>
{
    /// <summary>
    /// Gets or sets the unique identifier for the user. This property is used to distinguish between
    /// users in the system and is typically required for updating user information.
    /// </summary>
    public required int Id { get; set; } = id;
}