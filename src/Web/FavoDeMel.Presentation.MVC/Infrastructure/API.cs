using FavoDeMel.Presentation.MVC.Models.DTO;
using System;

namespace WebMVC.Infrastructure
{
    public static class API
    {
        public static class Pedido
        {

            public static string IniciarPedido(string baseUri)
            {
                return $"{baseUri}/api/venda/iniciar";
            }

            public static string ObterCarrinhoCliente(string baseUri, Guid clienteId)
            {
                return $"{baseUri}/api/venda/meu-carrinho/{clienteId}";
            }

            public static string ObterPedidosCliente(string baseUri, Guid clienteId)
            {
                return $"{baseUri}/api/venda/meu-carrinho/{clienteId}";
            }

            public static string AdicionarItemPedido(string baseUri)
            {
                return $"{baseUri}/api/venda/meu-carrinho/item/adicionar";
            }

            public static string AtualizarItemPedido(string baseUri)
            {
                return $"{baseUri}/api/venda/meu-carrinho/item/atualizar";
            }

            public static string RemoverItemPedido(string baseUri)
            {
                return $"{baseUri}/api/venda/meu-carrinho/item/remover";
            }

            
        }

        public static class Produto
        {

            public static string ObterCategorias(string baseUri)
            {
                return $"{baseUri}/api/produto/categorias";
            }

            public static string ObterPorCategoria(string baseUri, int codigo)
            {
                return $"{baseUri}/api/produto/categoria/{codigo}";
            }

            public static string ObterPorId(string baseUri, Guid id)
            {
                return $"{baseUri}/api/produto/produto-detalhe/{id}";
            }

            public static string ObterTodos(string baseUri)
            {
                return $"{baseUri}/api/produto/vitrine";
            }

            public static string Adicionar(string baseUri)
            {
                return $"{baseUri}/api/produto/adicionar";
            }

            public static string Atualizar(string baseUri)
            {
                return $"{baseUri}/api/produto/atualizar";
            }

            public static string DebitarEstoque(string baseUri)
            {
                return $"{baseUri}/api/produto/estoque/debitar";
            }

            public static string ReporEstoque(string baseUri)
            {
                return $"{baseUri}/api/produto/estoque/repor";
            }

        }

    }
}