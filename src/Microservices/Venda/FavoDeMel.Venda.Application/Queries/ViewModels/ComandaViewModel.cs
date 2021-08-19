using FavoDeMel.Venda.Domain.Models;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Venda.Application.Queries.ViewModels
{
    public class ComandaViewModel
    {
        public Guid ComandaId { get; set; }
        public Guid? MesaId { get; set; }
        public string Codigo { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public string VoucherCodigo { get; set; }
        public DateTime DataCadastro { get; set; }
        public ComandaStatus ComandaStatus { get; set; }
        public MesaViewModel Mesa { get; set; }
        public List<ComandaItemViewModel> Items { get; set; } = new List<ComandaItemViewModel>();
        public ComandaPagamentoViewModel Pagamento { get; set; }
    }
}