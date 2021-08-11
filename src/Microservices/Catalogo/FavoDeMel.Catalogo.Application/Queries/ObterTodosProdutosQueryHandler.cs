using MediatR;
using FavoDeMel.Catalogo.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FavoDeMel.Catalogo.Application.ViewModels;

namespace FavoDeMel.Catalogo.Application.Queries
{
    public class ObterTodosProdutosQueryHandler : IRequestHandler<ObterTodosProdutosQuery, IEnumerable<ProdutoViewModel>>
    {
        private readonly IProdutoFinder _finder;

        public ObterTodosProdutosQueryHandler(IProdutoFinder finder)
        {
            _finder = finder;
        }

        public async Task<IEnumerable<ProdutoViewModel>> Handle(ObterTodosProdutosQuery request, CancellationToken cancellationToken)
        {
            return await _finder.ObterTodos();
        }
    }
}
