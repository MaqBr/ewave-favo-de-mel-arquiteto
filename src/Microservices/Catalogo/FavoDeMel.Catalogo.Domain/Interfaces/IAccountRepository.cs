using FavoDeMel.Catalogo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoDeMel.Catalogo.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}
