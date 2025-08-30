using ApiDemarco.Api.Middlewares;
using ApiDemarco.Application.DTOs;
using ApiDemarco.Application.Features.Clientes.Commands;
using ApiDemarco.Application.Features.Clientes.Querys;
using ApiDemarco.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemarco.Api.Controllers;

[ApiController]
public class ClientesController(IMediator mediator) : ControllerBase
{
     /// <summary>
    /// Retrieves a list of customers.
    /// </summary>
    /// <param name="paginationParams">Optional parameter to specify pagination details.</param>
    /// <returns>Returns a paginated list of customers.</returns>
    [HttpGet("clientes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Status401Payload), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Status403Payload), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Status404Payload), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Status500Payload), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetClientes([FromQuery] PaginationParams? paginationParams = null)
    {
        var query = new GetClientesListQuery();
        var result = await mediator.Send(query);

        var pagedResult = await PaginationHelper.GetPagedResult(result, paginationParams!);

        return Ok(pagedResult);
    }
    
    /// <summary>
    /// Add Cliente.
    /// </summary>
    /// <param name="command">Object containing the customers's name, email.</param>
    /// <returns>A response indicating the outcome of the operation. Includes status codes 201 (Created), 401 (Unauthorized), 404 (Not Found), or 500 (Internal Server Error).</returns>
    [HttpPost("clientes")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Status401Payload), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Status404Payload), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Status500Payload), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateClienteCommand createcommand)
    {
        var result = await mediator.Send(createcommand);
        return Created(string.Empty, result);
    }
    
    /// <summary>
    /// Update Cliente.
    /// </summary>
    /// <param name="udpatecommand">Object containing the customer's name, email.</param>
    /// <returns>A response indicating the outcome of the operation. Includes status codes 200 (Ok), 401 (Unauthorized), 404 (Not Found), or 500 (Internal Server Error).</returns>
    [HttpPut("clientes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Status401Payload), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Status404Payload), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Status500Payload), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateClienteCommand updatecommand)
    {
        var result = await mediator.Send(updatecommand);
        return Ok(result);
    }
    
    /// <summary>
    /// Delete Cliente.
    /// </summary>
    /// <param name="deletecommand">Object containing the customer's id information.</param>
    /// <returns>A response indicating the outcome of the operation. Includes status codes 200 (Ok), 401 (Unauthorized), 404 (Not Found), or 500 (Internal Server Error).</returns>
    [HttpDelete("clientes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Status401Payload), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Status404Payload), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Status500Payload), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteClienteCommand deletecommand)
    {
        var result = await mediator.Send(deletecommand);
        return Ok(result);
    }
}