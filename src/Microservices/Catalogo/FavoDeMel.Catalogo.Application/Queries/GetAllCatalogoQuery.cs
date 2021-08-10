using MediatR;
using FavoDeMel.Catalogo.Domain.Models;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Catalogo.Application.Queries
{
    public class GetAllCatalogoQuery : IRequest<IEnumerable<CatalogoDTO>>
    {

    }
}
