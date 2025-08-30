using ApiDemarco.Application.DTOs;
using MediatR;

namespace ApiDemarco.Application.Features.Clientes.Querys;

public class GetClientesListQuery :  IRequest<List<ClienteDto>>
{
    
}