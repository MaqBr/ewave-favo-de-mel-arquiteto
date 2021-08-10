using BuildingBlocks.EventBus.Abstractions;
using FavoDeMel.Venda.Application.Interfaces;
using FavoDeMel.Venda.Domain.Interfaces;
using FavoDeMel.Venda.Domain.Models;
using System.Collections.Generic;

namespace FavoDeMel.Venda.Application.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _VendaRepository;
        private readonly IEventBus _bus;

        public VendaService(IVendaRepository VendaRepository, IEventBus bus)
        {
            _VendaRepository = VendaRepository;
            _bus = bus;
        }

        public IEnumerable<VendaLog> GetVendaLogs()
        {
            return _VendaRepository.GetVendaLogs();
        }
    }
}
