using FavoDeMel.Venda.Domain.Models;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Venda.Application.Queries.ViewModels
{
    public class MesaViewModel
    {
        public Guid MesaId { get; set; }
        public int Numero { get; set; }
        public DateTime DataCriacao { get; set; }
        public SituacaoMesa Situacao { get; set; }

    }
}