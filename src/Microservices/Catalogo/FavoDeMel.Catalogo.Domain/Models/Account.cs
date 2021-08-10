using FavoDeMel.Domain.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoDeMel.Catalogo.Domain.Models
{
    public class Account : Entity, IAggregateRoot
    {
        public string AccountType { get; private set; }
        public decimal AccountBalance { get; private set; }

        protected Account() { }
        public Account(string accountType, decimal accountBalance)
        {
            AccountType = accountType;
            AccountBalance = accountBalance;
            Validar();
        }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(AccountType, "O campo AccountType não pode estar vazio");
            Validacoes.ValidarSeMenorQue(AccountBalance, 0, "O campo AccountBalance não pode se menor igual a 0");

        }
    }
}
