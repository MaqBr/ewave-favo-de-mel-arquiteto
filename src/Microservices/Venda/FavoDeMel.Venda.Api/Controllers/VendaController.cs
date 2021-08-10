using System.Collections.Generic;
using FavoDeMel.Venda.Application.Interfaces;
using FavoDeMel.Venda.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace FavoDeMel.Venda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _VendaService;

        public VendaController(IVendaService VendaService)
        {
            _VendaService = VendaService;
        }

        // GET api/Venda
        [HttpGet]
        public ActionResult<IEnumerable<VendaLog>> Get()
        {
            return Ok(_VendaService.GetVendaLogs());
        }
    }
}
