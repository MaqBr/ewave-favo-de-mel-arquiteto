using System;
using FavoDeMel.Domain.Core.Messages;
using FavoDeMel.Venda.Domain.Models;

namespace FavoDeMel.Venda.Application
{
    public class AdicionarComandaCommand : Command
    {
        public Guid ComandaId { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid MesaId { get; set; }
        public string Codigo { get; private set; }

        public AdicionarComandaCommand(Guid comandaId, Guid mesaId, Guid clienteId, string codigo)
        {
            AggregateId = comandaId;
            ComandaId = comandaId;
            MesaId = mesaId;
            ClienteId = clienteId;
            Codigo = codigo;
        }
    }
}