using System;
using FluentValidation;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application
{
    public class IniciarComandaCommand : Command
    {
        public Guid ComandaId { get; private set; }
        public Guid MesaId { get; private set; }
        public decimal Total { get; private set; }

        public IniciarComandaCommand(Guid comandaId, Guid mesaId, decimal total)
        {
            ComandaId = comandaId;
            MesaId = mesaId;
            Total = total;
        }

        public override bool EhValido()
        {
            ValidationResult = new IniciarComandaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class IniciarComandaValidation : AbstractValidator<IniciarComandaCommand>
    {
        public IniciarComandaValidation()
        {
            RuleFor(c => c.MesaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da mesa inválido");

            RuleFor(c => c.ComandaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da comanda inválido");
        }
    }
}