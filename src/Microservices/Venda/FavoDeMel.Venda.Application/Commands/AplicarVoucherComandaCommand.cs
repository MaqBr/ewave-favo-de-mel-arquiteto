using System;
using FluentValidation;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application
{
    public class AplicarVoucherComandaCommand : Command
    {
        public Guid MesaId { get; private set; }
        public string CodigoVoucher { get; private set; }

        public AplicarVoucherComandaCommand(Guid mesaId, string codigoVoucher)
        {
            MesaId = mesaId;
            CodigoVoucher = codigoVoucher;
        }

        public override bool EhValido()
        {
            ValidationResult = new AplicarVoucherComandaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AplicarVoucherComandaValidation : AbstractValidator<AplicarVoucherComandaCommand>
    {
        public AplicarVoucherComandaValidation()
        {
            RuleFor(c => c.MesaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da mesa inválido");

            RuleFor(c => c.CodigoVoucher)
                .NotEmpty()
                .WithMessage("O código do voucher não pode ser vazio");
        }
    }
}