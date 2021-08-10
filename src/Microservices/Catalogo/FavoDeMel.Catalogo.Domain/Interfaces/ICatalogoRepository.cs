using FavoDeMel.Catalogo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FavoDeMel.Catalogo.Domain.Interfaces
{
    public interface ICatalogoRepository
    {
        IEnumerable<Models.Catalogo> GetCatalogos();
    }
}
