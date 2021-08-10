using MediatR;
using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Catalogo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.Queries
{
    public class GetAllCatalogoQueryHandler : IRequestHandler<GetAllCatalogoQuery, IEnumerable<CatalogoDTO>>
    {
        private readonly ICatalogoFinder _finder;

        public GetAllCatalogoQueryHandler(ICatalogoFinder finder)
        {
            _finder = finder;
        }

        public async Task<IEnumerable<CatalogoDTO>> Handle(GetAllCatalogoQuery request, CancellationToken cancellationToken)
        {
            return await _finder.GetAll();
        }
    }
}
