using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FavoDeMel.Domain.Core.Communication.Mediator;
using FavoDeMel.Domain.Core.Messages;
using FavoDeMel.Domain.Core.Messages.CommonMessages.IntegrationEvents;
using FavoDeMel.Domain.Core.Messages.CommonMessages.Notifications;
using FavoDeMel.Venda.Application.Events;
using FavoDeMel.Venda.Domain.Models;
using FavoDeMel.Domain.Core.DomainObjects.DTO;
using FavoDeMel.Catalogo.Data.Dapper.ExtensionsMethods;
using BuildingBlocks.EventBus.Abstractions;

namespace FavoDeMel.Venda.Application
{
    public class ComandaCommandHandler :
        IRequestHandler<AdicionarItemComandaCommand, bool>,
        IRequestHandler<AtualizarItemComandaCommand, bool>,
        IRequestHandler<RemoverItemComandaCommand, bool>,
        IRequestHandler<AplicarVoucherComandaCommand, bool>,
        IRequestHandler<IniciarComandaCommand, bool>,
        IRequestHandler<EntregarComandaCommand, bool>,
        IRequestHandler<FinalizarComandaCommand, bool>,
        IRequestHandler<AdicionarComandaCommand, bool>,
        IRequestHandler<CancelarComandaCommand, bool>
        
    {
        private readonly IComandaRepository _comandaRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IEventBus _bus;
        public ComandaCommandHandler(IComandaRepository comandaRepository, 
                                    IMediatorHandler mediatorHandler,
                                    IEventBus bus)
        {
            _comandaRepository = comandaRepository;
            _mediatorHandler = mediatorHandler;
            _bus = bus;
        }

        public async Task<bool> Handle(AdicionarItemComandaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var comanda = await _comandaRepository.ObterComandaPorMesaId(message.MesaId);
            var comandaItem = new ComandaItem(message.ProdutoId, message.Nome, message.Quantidade, message.ValorUnitario);

            if (comanda == null)
            {
                comanda = Comanda.ComandaFactory.NovaComandaRascunho(message.MesaId);
                comanda.AdicionarItem(comandaItem);

                _comandaRepository.Adicionar(comanda);
                comanda.AdicionarEvento(new ComandaRascunhoIniciadoEvent(message.MesaId, message.ProdutoId));
            }
            else
            {
                var comandaItemExistente = comanda.ComandaItemExistente(comandaItem);
                comanda.AdicionarItem(comandaItem);

                if (comandaItemExistente)
                {
                    _comandaRepository.AtualizarItem(comanda.ComandaItems.FirstOrDefault(p => p.ProdutoId == comandaItem.ProdutoId));
                }
                else
                {
                    _comandaRepository.AdicionarItem(comandaItem);
                }
            }

            comanda.AdicionarEvento(new ComandaItemAdicionadoEvent(comanda.MesaId.Value, comanda.Id, message.ProdutoId, message.Nome, message.ValorUnitario, message.Quantidade));
            _bus.Publish(new ComandaItemAdicionadoEvent(comanda.MesaId.Value, comanda.Id, message.ProdutoId, message.Nome, message.ValorUnitario, message.Quantidade));

            return await _comandaRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarItemComandaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var comanda = await _comandaRepository.ObterComandaPorMesaId(message.MesaId);

            if (comanda == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("comanda", "Comanda não encontrado!"));
                return false;
            }

            var comandaItem = await _comandaRepository.ObterItemPorComanda(comanda.Id, message.ProdutoId);

            if (!comanda.ComandaItemExistente(comandaItem))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("comanda", "Item do comanda não encontrado!"));
                return false;
            }

            comanda.AtualizarUnidades(comandaItem, message.Quantidade);
            comanda.AtualizarItemStatus(comandaItem, message.ItemStatus);
            comanda.AdicionarEvento(new ComandaProdutoAtualizadoEvent(message.MesaId, comanda.Id, message.ProdutoId, message.Quantidade, message.ItemStatus));
            _bus.Publish(new ComandaProdutoAtualizadoEvent(message.MesaId, comanda.Id, message.ProdutoId, message.Quantidade, message.ItemStatus));
            _comandaRepository.AtualizarItem(comandaItem);
            _comandaRepository.Atualizar(comanda);

            return await _comandaRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoverItemComandaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var comanda = await _comandaRepository.ObterComandaPorMesaId(message.MesaId);

            if (comanda == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("comanda", "Comanda não encontrado!"));
                return false;
            }

            var comandaItem = await _comandaRepository.ObterItemPorComanda(comanda.Id, message.ProdutoId);

            if (comandaItem != null && !comanda.ComandaItemExistente(comandaItem))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("comanda", "Item do comanda não encontrado!"));
                return false;
            }

            comanda.RemoverItem(comandaItem);
            comanda.AdicionarEvento(new ComandaProdutoRemovidoEvent(message.MesaId, comanda.Id, message.ProdutoId));
            //Publica em RabbitMQ para integração - subscriber em contexto de catálogo -> atualizar o estoque
            _bus.Publish(new ComandaProdutoRemovidoEvent(message.MesaId, comanda.Id, message.ProdutoId));
            _comandaRepository.RemoverItem(comandaItem);
            _comandaRepository.Atualizar(comanda);

            return await _comandaRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AplicarVoucherComandaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var comanda = await _comandaRepository.ObterComandaPorMesaId(message.MesaId);

            if (comanda == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("comanda", "Comanda não encontrado!"));
                return false;
            }

            var voucher = await _comandaRepository.ObterVoucherPorCodigo(message.CodigoVoucher);

            if (voucher == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("comanda", "Voucher não encontrado!"));
                return false;
            }

            var voucherAplicacaoValidation = comanda.AplicarVoucher(voucher);
            if (!voucherAplicacaoValidation.IsValid)
            {
                foreach (var error in voucherAplicacaoValidation.Errors)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            comanda.AdicionarEvento(new VoucherAplicadoComandaEvent(message.MesaId, comanda.Id, voucher.Id));

            _comandaRepository.Atualizar(comanda);

            return await _comandaRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(IniciarComandaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var comanda = await _comandaRepository.ObterComandaPorMesaId(message.MesaId);
            comanda.IniciarComanda();

            var itensList = new List<Item>();
            comanda.ComandaItems.ForEach(i => itensList.Add(new Item { Id = i.ProdutoId, Quantidade = i.Quantidade }));
            var listaProdutosComanda = new ListaProdutosComanda { ComandaId = comanda.Id, Itens = itensList };

            comanda.AdicionarEvento(new ComandaIniciadaEvent(comanda.Id, comanda.MesaId.Value, listaProdutosComanda, comanda.ValorTotal));
            _comandaRepository.Atualizar(comanda);
            return await _comandaRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(EntregarComandaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var comanda = await _comandaRepository.ObterComandaPorMesaId(message.MesaId);
            comanda.EntregarComanda();

            var itensList = new List<Item>();
            comanda.ComandaItems.ForEach(i => itensList.Add(new Item { Id = i.ProdutoId, Quantidade = i.Quantidade }));
            var listaProdutosComanda = new ListaProdutosComanda { ComandaId = comanda.Id, Itens = itensList };

            comanda.AdicionarEvento(new ComandaEntregueEvent(comanda.Id, comanda.MesaId.Value, listaProdutosComanda, comanda.ValorTotal));
            _comandaRepository.Atualizar(comanda);
            return await _comandaRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AdicionarComandaCommand message, CancellationToken cancellationToken)
        {
            var comanda = await _comandaRepository.ObterPorId(message.ComandaId);

            if (comanda == null)
            {
                comanda = Comanda.ComandaFactory.NovaComanda(message.ComandaId, message.MesaId, message.Codigo);
                _comandaRepository.Adicionar(comanda);
                _comandaRepository.AtualizarMesa(message.MesaId, SituacaoMesa.Ocupada);

                comanda.AdicionarEvento(new ComandaAdicionadaEvent(message.ComandaId));
                return await _comandaRepository.UnitOfWork.Commit();
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Handle(FinalizarComandaCommand message, CancellationToken cancellationToken)
        {
            var comanda = await _comandaRepository.ObterPorId(message.ComandaId);

            if (comanda == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("comanda", "Comanda não encontrado!"));
                return false;
            }

            comanda.FinalizarComanda();
            _comandaRepository.AtualizarMesa(message.MesaId, SituacaoMesa.Livre);

            comanda.AdicionarEvento(new ComandaFinalizadaEvent(message.ComandaId));
            _bus.Publish(new ComandaFinalizadaEvent(message.ComandaId));
            return await _comandaRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelarComandaCommand message, CancellationToken cancellationToken)
        {
            var comanda = await _comandaRepository.ObterPorId(message.ComandaId);

            if (comanda == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("comanda", "Comanda não encontrado!"));
                return false;
            }

            comanda.CancelarComanda();
            _comandaRepository.AtualizarMesa(message.MesaId, SituacaoMesa.Livre);

            comanda.AdicionarEvento(new ComandaCanceladaEvent(message.ComandaId));
            _bus.Publish(new ComandaCanceladaEvent(message.ComandaId));
            return await _comandaRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }

    }
}