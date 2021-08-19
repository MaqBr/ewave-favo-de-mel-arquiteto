using System;
using FluentValidation;
using FavoDeMel.Domain.Core.Messages;
using FavoDeMel.Domain.Core.DomainObjects;

namespace FavoDeMel.Venda.Application
{
    public class AtualizarItemComandaCommand : Command
    {
        public Guid MesaId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public ItemStatus ItemStatus { get; private set; }

        public AtualizarItemComandaCommand(Guid mesaId, Guid produtoId, int quantidade, ItemStatus itemStatus)
        {
            MesaId = mesaId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            ItemStatus = itemStatus;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarItemComandaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AtualizarItemComandaValidation : AbstractValidator<AtualizarItemComandaCommand>
    {
        public AtualizarItemComandaValidation()
        {
            RuleFor(c => c.MesaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da mesa inválido");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.Quantidade)
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15");
        }
    }
}