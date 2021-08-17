using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using FavoDeMel.Domain.Core.DomainObjects;

namespace FavoDeMel.Venda.Domain.Models
{
    public class Comanda : Entity, IAggregateRoot
    {
        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public ComandaStatus ComandaStatus { get; private set; }

        private readonly List<ComandaItem> _comandaItems;
        public IReadOnlyCollection<ComandaItem> ComandaItems => _comandaItems;

        // EF Rel.
        public Voucher Voucher { get; private set; }

        public Comanda(Guid clienteId, bool voucherUtilizado, decimal desconto, decimal valorTotal)
        {
            ClienteId = clienteId;
            VoucherUtilizado = voucherUtilizado;
            Desconto = desconto;
            ValorTotal = valorTotal;
            _comandaItems = new List<ComandaItem>();
        }

        protected Comanda()
        {
            _comandaItems = new List<ComandaItem>();
        }

        public ValidationResult AplicarVoucher(Voucher voucher)
        {
            var validationResult = voucher.ValidarSeAplicavel();
            if (!validationResult.IsValid) return validationResult;

            Voucher = voucher;
            VoucherUtilizado = true;
            CalcularValorPedido();

            return validationResult;
        }

        public void CalcularValorPedido()
        {
            ValorTotal = ComandaItems.Sum(p => p.CalcularValor());
            CalcularValorTotalDesconto();
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            decimal desconto = 0;
            var valor = ValorTotal;

            if (Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Porcentagem)
            {
                if (Voucher.Percentual.HasValue)
                {
                    desconto = (valor * Voucher.Percentual.Value) / 100;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }

            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }

        public bool ComandaItemExistente(ComandaItem item)
        {
            return _comandaItems.Any(p => p.ProdutoId == item.ProdutoId);
        }

        public void AdicionarItem(ComandaItem item)
        {
            if (!item.EhValido()) return;

            item.AssociarPedido(Id);

            if (ComandaItemExistente(item))
            {
                var itemExistente = _comandaItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
                itemExistente.AdicionarUnidades(item.Quantidade);
                item = itemExistente;

                _comandaItems.Remove(itemExistente);
            }

            item.CalcularValor();
            _comandaItems.Add(item);

            CalcularValorPedido();
        }

        public void RemoverItem(ComandaItem item)
        {
            if (!item.EhValido()) return;

            var itemExistente = ComandaItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

            if (itemExistente == null) throw new DomainException("O item não pertence ao pedido");
            _comandaItems.Remove(itemExistente);

            CalcularValorPedido();
        }

        public void AtualizarItem(ComandaItem item)
        {
            if (!item.EhValido()) return;
            item.AssociarPedido(Id);

            var itemExistente = ComandaItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

            if (itemExistente == null) throw new DomainException("O item não pertence ao pedido");

            _comandaItems.Remove(itemExistente);
            _comandaItems.Add(item);

            CalcularValorPedido();
        }

        public void AtualizarUnidades(ComandaItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }

        public void AtualizarItemStatus(ComandaItem item, ItemStatus itemStatus)
        {
            item.AtualizarItemStatus(itemStatus);
            AtualizarItem(item);
        }

        public void TornarRascunho()
        {
            ComandaStatus = ComandaStatus.Rascunho;
        }

        public void IniciarPedido()
        {
            ComandaStatus = ComandaStatus.Iniciado;
        }

        public void FinalizarPedido()
        {
            ComandaStatus = ComandaStatus.Pago;
        }

        public void CancelarPedido()
        {
            ComandaStatus = ComandaStatus.Cancelado;
        }

        public static class ComandaFactory
        {
            public static Comanda NovaComandaRascunho(Guid clienteId)
            {
                var pedido = new Comanda
                {
                    ClienteId = clienteId,
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }
    }
}