using FavoDeMel.Catalogo.Application.Models;
using FavoDeMel.Catalogo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.Interfaces
{
    public interface ICatalogoFinder
    {
        Task<IEnumerable<AccountDTO>> GetAll();
    }
}
