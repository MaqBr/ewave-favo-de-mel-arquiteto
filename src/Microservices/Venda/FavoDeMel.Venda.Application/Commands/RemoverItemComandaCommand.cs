using System;
using FluentValidation;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application
{
    public class RemoverItemComandaCommand : Command
    {
        public Guid MesaId { get; private set; }
        public Guid ProdutoId { get; private set; }

        public RemoverItemComandaCommand(Guid mesaId, Guid produtoId)
        {
            MesaId = mesaId;
            ProdutoId = produtoId;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverItemComandaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoverItemComandaValidation : AbstractValidator<RemoverItemComandaCommand>
    {
        public RemoverItemComandaValidation()
        {
            RuleFor(c => c.MesaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da mesa inválido");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");
        }
    }

    
}