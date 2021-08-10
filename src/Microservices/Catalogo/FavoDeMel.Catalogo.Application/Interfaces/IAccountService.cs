using FavoDeMel.Catalogo.Application.Models;
using FavoDeMel.Catalogo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAccounts();
        void Transfer(AccountTransfer accountTransfer);
    }
}
