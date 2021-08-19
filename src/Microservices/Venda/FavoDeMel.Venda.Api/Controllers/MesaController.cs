using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FavoDeMel.Venda.Application.Queries;
using FavoDeMel.Venda.Application.Queries.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FavoDeMel.Venda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {

        private readonly IMesaQueries _mesaQueries;
        private readonly ILogger<MesaController> _logger;

        public MesaController(IMesaQueries mesaQueries, 
            ILogger<MesaController> logger)
        {
            _logger = logger;
            _mesaQueries = mesaQueries;
        }

        [HttpGet]
        [Route("obter/todos")]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var mesas = await _mesaQueries.ObterTodos();
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("obter/detalhe/{id}")]
        public async Task<MesaViewModel> ObterPorId(Guid id)
        {
            try
            {
                var result = await _mesaQueries.ObterPorId(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Houve um erro ao obter os dados");
                throw new Exception($"Houve um erro ao obter os dados: {ex.Message}");
            }

        }

    }
}
