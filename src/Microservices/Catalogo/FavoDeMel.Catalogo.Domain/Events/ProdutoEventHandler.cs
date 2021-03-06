using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;

namespace FavoDeMel.Catalogo.Domain.Events
{
    public class ProdutoEventHandler : 
        INotificationHandler<ProdutoAbaixoEstoqueEvent>,
        INotificationHandler<ComandaIniciadaEvent>,
        INotificationHandler<ComandaProcessamentoCanceladoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMediatorHandler _mediatorHandler;

        public ProdutoEventHandler(IProdutoRepository produtoRepository, 
                                   IEstoqueService estoqueService, 
                                   IMediatorHandler mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent mensagem, CancellationToken cancellationToken)
        {
            await _produtoRepository.ObterPorId(mensagem.AggregateId);
        }

        public async Task Handle(ComandaIniciadaEvent message, CancellationToken cancellationToken)
        {
            var result = await _estoqueService.DebitarListaProdutosComanda(message.ProdutosComanda);
        }

        public async Task Handle(ComandaProcessamentoCanceladoEvent message, CancellationToken cancellationToken)
        {
            await _estoqueService.ReporListaProdutosComanda(message.ProdutosComanda);
        }
    }
}
