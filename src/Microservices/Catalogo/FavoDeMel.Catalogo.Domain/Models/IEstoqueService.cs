using System;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.DomainObjects.DTO;

namespace FavoDeMel.Catalogo.Domain
{
    public interface IEstoqueService : IDisposable
    {
        Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
        Task<bool> DebitarListaProdutosComanda(ListaProdutosComanda lista);
        Task<bool> ReporEstoque(Guid produtoId, int quantidade);
        Task<bool> ReporListaProdutosComanda(ListaProdutosComanda lista);
    }
}