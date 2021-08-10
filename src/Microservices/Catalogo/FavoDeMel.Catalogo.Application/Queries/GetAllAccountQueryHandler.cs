using MediatR;
using FavoDeMel.Catalogo.Application.Interfaces;
using FavoDeMel.Catalogo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FavoDeMel.Catalogo.Application.Queries
{
    public class GetAllAccountQueryHandler : IRequestHandler<GetAllAccountQuery, IEnumerable<AccountDTO>>
    {
        private readonly ICatalogoFinder _finder;

        public GetAllAccountQueryHandler(ICatalogoFinder finder)
        {
            _finder = finder;
        }

        public async Task<IEnumerable<AccountDTO>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
        {
            return await _finder.GetAll();
        }
    }
}
