using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Data;

namespace FavoDeMel.Venda.Domain.Models
{
    public interface IComandaRepository : IRepository<Comanda>
    {
        Task<Comanda> ObterPorId(Guid id);
        Task<IEnumerable<Comanda>> ObterListaPorMesaId(Guid mesaId);
        Task<IEnumerable<Comanda>> ObterListaPorStatus(ComandaStatus status);
        Task<Comanda> ObterComandaRascunhoPorMesaId(Guid mesaId);
        void Adicionar(Comanda comanda);
        void Atualizar(Comanda comanda);

        Task<ComandaItem> ObterItemPorId(Guid id);
        Task<ComandaItem> ObterItemPorComanda(Guid comandaId, Guid produtoId);
        void AdicionarItem(ComandaItem comandaItem);
        void AtualizarItem(ComandaItem comandaItem);
        void RemoverItem(ComandaItem comandaItem);
        void AtualizarMesa(Guid mesaId, SituacaoMesa situacaoMesa);

        Task<Voucher> ObterVoucherPorCodigo(string codigo);
    }
}