using FavoDeMel.Catalogo.Domain;
using System.Data;

namespace FavoDeMel.Catalogo.Data.Dapper.Repositorio
{
    public class CatalogoRepository : RepositoryBase<Produto>
    {
        public CatalogoRepository()
        { }

        public CatalogoRepository(IDbTransaction transaction)
            : base(transaction)
        { }
    }
}
