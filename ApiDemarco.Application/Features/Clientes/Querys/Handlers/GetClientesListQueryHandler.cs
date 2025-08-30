using ApiDemarco.Application.DTOs;
using ApiDemarco.Application.Logging;
using ApiDemarco.Domain.Repositories.Interfaces.Read.Clientes;
using MediatR;

namespace ApiDemarco.Application.Features.Clientes.Querys.Handlres;

public class GetClientesListQueryHandler(
    IClienteReadRepository clienteReadRepository,
    IAppLogger<GetClientesListQueryHandler> logger) : IRequestHandler<GetClientesListQuery, List<ClienteDto>>
{
    public async Task<List<ClienteDto>> Handle(GetClientesListQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var clientes = await clienteReadRepository.GetClientesListAsync();

            var usersDto = new List<ClienteDto>();

            usersDto = clientes.Select(cliente => new ClienteDto()
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
            }).ToList();

            return usersDto;
        }
        catch (Exception ex)
        {
            logger.LogError("Erro ao obter lista de usuários", ex);
            throw;
        }
    }
}