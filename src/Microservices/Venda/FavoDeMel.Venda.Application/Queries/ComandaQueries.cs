using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoDeMel.Venda.Application.Queries.ViewModels;
using FavoDeMel.Venda.Domain.Models;

namespace FavoDeMel.Venda.Application.Queries
{
    public class ComandaQueries : IComandaQueries
    {
        private readonly IComandaRepository _pedidoRepository;

        public ComandaQueries(IComandaRepository comandaRepository)
        {
            _pedidoRepository = comandaRepository;
        }

        public async Task<ComandaViewModel> ObterComandaCliente(Guid clienteId)
        {
            var comanda = await _pedidoRepository.ObterComandaRascunhoPorClienteId(clienteId);
            if (comanda == null) return null;

            var carrinho = new ComandaViewModel
            {
                ClienteId = comanda.ClienteId,
                ValorTotal = comanda.ValorTotal,
                ComandaId = comanda.Id,
                ValorDesconto = comanda.Desconto,
                SubTotal = comanda.Desconto + comanda.ValorTotal
            };

            if (comanda.VoucherId != null)
            {
                carrinho.VoucherCodigo = comanda.Voucher.Codigo;
            }

            foreach (var item in comanda.ComandaItems)
            {
                carrinho.Items.Add(new ComandaItemViewModel
                {
                    ProdutoId = item.ProdutoId,
                    ProdutoNome = item.ProdutoNome,
                    ItemStatus = item.ItemStatus,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorUnitario * item.Quantidade
                });
            }

            return carrinho;
        }

        public async Task<IEnumerable<ComandaViewModel>> ObterPedidosCliente(Guid clienteId)
        {
            var comandas = await _pedidoRepository.ObterListaPorClienteId(clienteId);

            comandas = comandas.Where(p => p.ComandaStatus == ComandaStatus.Pago || p.ComandaStatus == ComandaStatus.Cancelado)
                .OrderByDescending(p => p.Codigo);

            if (!comandas.Any()) return null;

            var pedidosView = new List<ComandaViewModel>();

            foreach (var comanda in comandas)
            {
                pedidosView.Add(new ComandaViewModel
                {
                    ComandaId = comanda.Id,
                    ValorTotal = comanda.ValorTotal,
                    ComandaStatus = comanda.ComandaStatus,
                    Codigo = comanda.Codigo,
                    DataCadastro = comanda.DataCadastro
                });
            }

            return pedidosView;
        }

        public async Task<IEnumerable<ComandaViewModel>> ObterComandaStatus(ComandaStatus status)
        {
            var comandas = await _pedidoRepository.ObterListaPorStatus(status);

            comandas = comandas.Where(p => p.ComandaStatus == status)
                .OrderByDescending(p => p.Codigo);

            if (!comandas.Any()) return null;

            var pedidosView = new List<ComandaViewModel>();

            foreach (var comanda in comandas)
            {
                pedidosView.Add(new ComandaViewModel
                {
                    ComandaId = comanda.Id,
                    ValorTotal = comanda.ValorTotal,
                    ComandaStatus = comanda.ComandaStatus,
                    Codigo = comanda.Codigo,
                    DataCadastro = comanda.DataCadastro
                });
            }

            return pedidosView;
        }
    }
}