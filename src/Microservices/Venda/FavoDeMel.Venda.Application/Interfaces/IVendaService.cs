using FavoDeMel.Venda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoDeMel.Venda.Application.Interfaces
{
    public interface IVendaService
    {
        IEnumerable<VendaLog> GetVendaLogs();
    }
}
