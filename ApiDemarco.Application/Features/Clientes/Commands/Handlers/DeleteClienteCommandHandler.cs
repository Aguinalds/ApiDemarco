using ApiDemarco.Application.Exceptions;
using ApiDemarco.Application.Logging;
using ApiDemarco.Domain.Entities;
using ApiDemarco.Domain.Repositories.Interfaces.Read.Clientes;
using ApiDemarco.Domain.Repositories.Interfaces.Write.Clientes;
using DotExtension.Application.Logging;
using MediatR;

namespace ApiDemarco.Application.Features.Clientes.Commands.Handlers;

public class DeleteClienteCommandHandler(
    IClienteWriteRepository clienteWriteRepository,
    IClienteReadRepository clienteReadRepository,
    IAppLogger<DeleteClienteCommand> logger) : IRequestHandler<DeleteClienteCommand, string>
{
    public async Task<string> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        var clienteExist = await clienteReadRepository.GetByIdAsync(request.Id);

        if (clienteExist == null)
        {
            throw new ClientesException.ClienteNotFoundException();
        }

        var cliente = new Cliente
        {
            Id = request.Id,
        };

        var clienteDelete = await clienteWriteRepository.DeleteAsync(cliente, cancellationToken);
        try
        {
            if (clienteDelete != null)
            {
                logger.LogSuccess("Cliente deletado com sucesso", clienteDelete.Id);
                return $"Cliente Deletado com Sucesso!";
            }

            throw new ClientesException.ClienteDeleteErrorException();
        }
        catch (Exception ex)
        {
            logger.LogError(LogMessages.ClienteDeleteError, ex, request.Id);
            throw;
        }
    }
}