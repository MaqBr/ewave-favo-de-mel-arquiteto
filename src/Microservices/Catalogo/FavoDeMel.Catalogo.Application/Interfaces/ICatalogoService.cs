using FavoDeMel.Catalogo.Application.Models;
using FavoDeMel.Catalogo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.Interfaces
{
    public interface ICatalogoService
    {
        Task<IEnumerable<CatalogoDTO>> GetAccounts();
        void Venda(AccountVenda accountVenda);
    }
}
