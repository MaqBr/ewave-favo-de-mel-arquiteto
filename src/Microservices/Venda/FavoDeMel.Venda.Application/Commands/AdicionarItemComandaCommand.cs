using System;
using FavoDeMel.Domain.Core.Messages;
using FluentValidation;

namespace FavoDeMel.Venda.Application
{
    public class AdicionarItemComandaCommand : Command
    {
        public Guid MesaId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public AdicionarItemComandaCommand(Guid mesaId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
        {
            MesaId = mesaId;
            ProdutoId = produtoId;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarItemComandaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarItemComandaValidation : AbstractValidator<AdicionarItemComandaCommand>
    {
        public AdicionarItemComandaValidation()
        {
            RuleFor(c => c.MesaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da mesa inválido");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.Quantidade)
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15");

            RuleFor(c => c.ValorUnitario)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");
        }
    }
}