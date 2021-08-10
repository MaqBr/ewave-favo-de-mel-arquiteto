using BuildingBlocks.EventBus.Abstractions;
using MediatR;
using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Catalogo.Application.Models;
using FavoDeMel.Catalogo.Application.Queries;
using FavoDeMel.Catalogo.Domain.Commands;
using FavoDeMel.Catalogo.Domain.Interfaces;
using FavoDeMel.Catalogo.Domain.Models;
using FavoDeMel.Domain.Core.Communication.Mediator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly ICatalogoRepository _accountRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMediator _mediator;
        private readonly ILogger<CatalogoService> _logger;

        public CatalogoService(ICatalogoRepository accountRepository, 
            IMediatorHandler mediatorHandler,
            IMediator mediator,
            ILogger<CatalogoService> logger)
        {
            _accountRepository = accountRepository;
            _mediatorHandler = mediatorHandler;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogoDTO>> GetAccounts()
        {
            var command = new GetAllCatalogoQuery();
            var response = await _mediator.Send(command);
            return response;
        }

        public void Venda(AccountVenda accountVenda)
        {
            try
            {
                var createVendaCommand = new CreateVendaCommand(
                    accountVenda.FromAccount,
                    accountVenda.ToAccount,
                    accountVenda.VendaAmount
                );

                _mediatorHandler.EnviarComando(createVendaCommand);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer a tranferência de valores {accountVenda}", accountVenda);
                throw new Exception("Erro ao fazer a tranferência de valores");
            }
        }
    }
}
