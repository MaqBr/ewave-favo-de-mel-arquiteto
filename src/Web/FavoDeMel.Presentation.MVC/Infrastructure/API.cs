﻿using System;

namespace WebMVC.Infrastructure
{
    public static class API
    {
        public static class Pedido
        {
            public static string ObterCarrinhoCliente(string baseUri, Guid clienteId)
            {
                return $"{baseUri}/pedido/meus-pedidos/{clienteId}";
            }

            public static string ObterPedidosCliente(string baseUri, Guid clienteId)
            {
                return $"{baseUri}/pedido/cliente/{clienteId}";
            }
        }

        public static class Produto
        {
            public static string ObterPorCategoria(string baseUri, int codigo)
            {
                return $"{baseUri}/produto-detalhe/categoria/{codigo}";
            }

            public static string ObterPorId(string baseUri, Guid id)
            {
                return $"{baseUri}/id={id}";
            }

            public static string ObterTodos(string baseUri)
            {
                return $"{baseUri}/admin-produtos";
            }

            public static string Adicionar(string baseUri)
            {
                return $"{baseUri}/novo-produto";
            }

            public static string Editar(string baseUri)
            {
                return $"{baseUri}/editar-produto";
            }

            public static string AtualizarEstoque(string baseUri)
            {
                return $"{baseUri}/produtos-atualizar-estoque";
            }

        }

    }
}