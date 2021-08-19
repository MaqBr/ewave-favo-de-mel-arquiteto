using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Data;

namespace FavoDeMel.Venda.Domain.Models
{
    public interface IMesaRepository : IRepository<Mesa>
    {
        Task<IEnumerable<Mesa>> ObterTodos();
        Task<Mesa> ObterPorId(Guid id);
        void Adicionar(Mesa mesa);
        void Atualizar(Mesa mesa);
        void Remover(Mesa mesa);
    }
}