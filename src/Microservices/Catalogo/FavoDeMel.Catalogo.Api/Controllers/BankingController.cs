using System;
using System.Threading.Tasks;
using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Catalogo.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FavoDeMel.Catalogo.Api.Controllers
{
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<CatalogoController> _logger;

        public CatalogoController(IAccountService accountService, ILogger<CatalogoController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        // GET api/Catalogo
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                _logger.LogTrace("Executando o método get para obter contas");
                var accounts = await _accountService.GetAccounts();
                return new JsonResult(accounts);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountTransfer accountTransfer)
        {
            try
            {
                _logger.LogTrace("Executando o método post para transferência entre contas");
                _accountService.Transfer(accountTransfer);
                return Ok(accountTransfer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao enviar os dados {accountTransfer}", accountTransfer);
                throw new Exception($"Houve um erro ao enviar os dados: {ex.Message}");
            }

        }
    }
}
