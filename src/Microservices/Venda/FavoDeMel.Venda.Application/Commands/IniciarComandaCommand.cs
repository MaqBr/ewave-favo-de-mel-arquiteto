using System;
using FluentValidation;
using FavoDeMel.Domain.Core.Messages;

namespace FavoDeMel.Venda.Application
{
    public class IniciarComandaCommand : Command
    {
        public Guid ComandaId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal Total { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }

        public IniciarComandaCommand(Guid comandaId, Guid clienteId, decimal total, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            ComandaId = comandaId;
            ClienteId = clienteId;
            Total = total;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
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
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.ComandaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da comanda inválido");

            RuleFor(c => c.NomeCartao)
                .NotEmpty()
                .WithMessage("O nome no cartão não foi informado");

            RuleFor(c => c.NumeroCartao)
                .CreditCard()
                .WithMessage("Número de cartão de crédito inválido");

            RuleFor(c => c.ExpiracaoCartao)
                .NotEmpty()
                .WithMessage("Data de expiração não informada");

            RuleFor(c => c.CvvCartao)
                .Length(3, 4)
                .WithMessage("O CVV não foi preenchido corretamente");
        }
    }
}