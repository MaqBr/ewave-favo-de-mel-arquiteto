using FavoDeMel.Catalogo.Data.EF.Context;
using FavoDeMel.Catalogo.Domain.Interfaces;
using FavoDeMel.Catalogo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoDeMel.Catalogo.Data.EF.Repository
{
    public class CatalogoRepository : ICatalogoRepository
    {
        private CatalogoDbContext _ctx;

        public CatalogoRepository(CatalogoDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Domain.Models.Catalogo> GetCatalogos()
        {
            return _ctx.Accounts;
        }
    }
}
