using ApiDemarco.Application.Exceptions;
using ApiDemarco.Application.Logging;
using ApiDemarco.Domain.Entities;
using ApiDemarco.Domain.Repositories.Interfaces.Read.Clientes;
using ApiDemarco.Domain.Repositories.Interfaces.Write.Clientes;
using DotExtension.Application.Logging;
using MediatR;

namespace ApiDemarco.Application.Features.Clientes.Commands.Handlers;

public class UpdateClienteCommandHandler(IClienteWriteRepository clienteWriteRepository, IClienteReadRepository clienteReadRepository, IAppLogger<UpdateClienteCommandHandler> logger) : IRequestHandler<UpdateClienteCommand, string>
{
    public async Task<string> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        var clienteExist = await clienteReadRepository.GetByIdAsync(request.Id);

        if (clienteExist == null)
        {
            throw new ClientesException.ClienteNotFoundException();
        }
        
        var emailExists = await clienteReadRepository.GetByEmailAsync(request.Email);

        if (emailExists != null && emailExists.Id != request.Id)
        {
            throw new ClientesException.EmailExistErrorException();
        }
        
        var cliente = new Cliente()
        {
            Id = request.Id,
            Nome = request.Nome,
            Email = request.Email,
        };

        var clienteUpdate = await clienteWriteRepository.UpdateAsync(cliente, cancellationToken);
        try
        {
            if (clienteUpdate == null)
                throw new ClientesException.ClienteUpdateErrorException(request.Nome);
            
            logger.LogSuccess("Cliente atualizado com sucesso", clienteUpdate.Id);
            return $"Cliente {request.Nome} Atualizado com Sucesso!";
        }
        catch (Exception ex)
        {
            logger.LogError(LogMessages.ClienteUpdateError, ex, request.Nome);
            throw;
        }
    }
}