﻿using MediatR;
using System;
using System.Collections.Generic;
using FavoDeMel.Catalogo.Application.ViewModels;

namespace FavoDeMel.Catalogo.Application.Queries
{
    public class ObterTodosProdutosQuery : IRequest<IEnumerable<ProdutoViewModel>>
    {

    }
}
