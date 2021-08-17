using System;

namespace FavoDeMel.Domain.Core.DomainObjects.DTO
{
    public class PagamentoComanda
    {
        public Guid ComandaId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal Total { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
    }
}