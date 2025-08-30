using ApiDemarco.Application.Exceptions;
using ApiDemarco.Application.Logging;
using ApiDemarco.Domain.Entities;
using ApiDemarco.Domain.Repositories.Interfaces.Read.Clientes;
using ApiDemarco.Domain.Repositories.Interfaces.Write.Clientes;
using DotExtension.Application.Logging;
using MediatR;

namespace ApiDemarco.Application.Features.Clientes.Commands.Handlers;

public class CreateClienteCommandHandler(IClienteWriteRepository clienteWriteRepository,IClienteReadRepository clienteReadRepository, IAppLogger<CreateClienteCommandHandler> logger) : IRequestHandler<CreateClienteCommand, string>
{
    public async Task<string> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await clienteReadRepository.GetByEmailAsync(request.Email);

        if (emailExists != null)
            throw new ClientesException.EmailExistErrorException();

        var cliente = new Cliente
        {
            Nome = request.Nome,
            Email = request.Email,
        };
        
        try
        {
            var clienteCreate = await clienteWriteRepository.AddAsync(cliente, cancellationToken);
            if (clienteCreate == null)
                throw new ClientesException.ClienteCreateErrorException(request.Nome);
            
            logger.LogSuccess("Cliente cadastrado com sucesso", clienteCreate.Id);
            return $"Cliente {request.Nome} criado com sucesso!";
        }
        catch (Exception ex)
        {
            logger.LogError(LogMessages.ClienteCreateError, ex, request.Nome);
            throw;
        }
    }
}