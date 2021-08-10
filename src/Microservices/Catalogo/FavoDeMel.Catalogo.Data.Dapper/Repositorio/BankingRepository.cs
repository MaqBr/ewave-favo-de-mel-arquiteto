using FavoDeMel.Catalogo.Application.Models;
using System.Data;

namespace FavoDeMel.Catalogo.Data.Dapper.Repositorio
{
    public class CatalogoRepository : RepositoryBase<AccountVenda>
    {
        public CatalogoRepository()
        { }

        public CatalogoRepository(IDbTransaction transaction)
            : base(transaction)
        { }
    }
}
