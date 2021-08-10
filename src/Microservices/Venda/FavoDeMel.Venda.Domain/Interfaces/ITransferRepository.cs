using FavoDeMel.Venda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Venda.Domain.Interfaces
{
    public interface IVendaRepository
    {
        IEnumerable<VendaLog> GetVendaLogs();
        void Add(VendaLog VendaLog);
    }
}
