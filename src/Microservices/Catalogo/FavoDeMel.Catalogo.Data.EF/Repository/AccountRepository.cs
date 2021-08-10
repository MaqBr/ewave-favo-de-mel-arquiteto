using FavoDeMel.Catalogo.Data.EF.Context;
using FavoDeMel.Catalogo.Domain.Interfaces;
using FavoDeMel.Catalogo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoDeMel.Catalogo.Data.EF.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private CatalogoDbContext _ctx;

        public AccountRepository(CatalogoDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _ctx.Accounts;
        }
    }
}
