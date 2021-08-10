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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMediator _mediator;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, 
            IMediatorHandler mediatorHandler,
            IMediator mediator,
            ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _mediatorHandler = mediatorHandler;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<IEnumerable<AccountDTO>> GetAccounts()
        {
            var command = new GetAllAccountQuery();
            var response = await _mediator.Send(command);
            return response;
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            try
            {
                var createTransferCommand = new CreateTransferCommand(
                    accountTransfer.FromAccount,
                    accountTransfer.ToAccount,
                    accountTransfer.TransferAmount
                );

                _mediatorHandler.EnviarComando(createTransferCommand);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer a tranferência de valores {accountTransfer}", accountTransfer);
                throw new Exception("Erro ao fazer a tranferência de valores");
            }
        }
    }
}
