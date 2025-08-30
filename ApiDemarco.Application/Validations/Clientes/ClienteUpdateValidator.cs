using ApiDemarco.Application.Features.Clientes.Commands;
using FluentValidation;

namespace ApiDemarco.Application.Validations.Clientes;

public class ClienteUpdateValidator : AbstractValidator<UpdateClienteCommand>
{
    public ClienteUpdateValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O id é obrigatório.");
        
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome precisa de mais de 3 caracteres.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado é inválido.");
    }
}