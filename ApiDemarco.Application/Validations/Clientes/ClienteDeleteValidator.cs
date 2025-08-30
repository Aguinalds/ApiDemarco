using ApiDemarco.Application.Features.Clientes.Commands;
using FluentValidation;

namespace ApiDemarco.Application.Validations.Clientes;

public class ClienteDeleteValidator : AbstractValidator<DeleteClienteCommand>
{
    public ClienteDeleteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O id é obrigatório.");
    }
}