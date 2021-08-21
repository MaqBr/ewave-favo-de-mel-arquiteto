using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.ViewModels;
using System;

namespace WebMVC.Infrastructure
{
    public static class API
    {
        public static class Cozinha
        {

            public static string ObterComandaStatus(string baseUri, ComandaStatus status)
            {
                return $"{baseUri}/api/comanda/status/{status}";
            }

            public static string ObterComandaMesa(string baseUri, Guid mesaId)
            {
                return $"{baseUri}/api/comanda/mesa/{mesaId}";
            }


        }
        public static class Comanda { 

            public static string IniciarComanda(string baseUri)
            {
                return $"{baseUri}/api/comanda/iniciar";
            }

            public static string FinalizarComanda(string baseUri)
            {
                return $"{baseUri}/api/comanda/finalizar";
            }

            public static string CancelarComanda(string baseUri)
            {
                return $"{baseUri}/api/comanda/cancelar";
            }

            public static string ObterComandaMesa(string baseUri, Guid mesaId)
            {
                return $"{baseUri}/api/comanda/mesa/{mesaId}";
            }

            public static string ObterComandaStatus(string baseUri, ComandaStatus status)
            {
                return $"{baseUri}/api/comanda/status/{status}";
            }

            public static string AdicionarComanda(string baseUri)
            {
                return $"{baseUri}/api/comanda/adicionar";
            }

            public static string AdicionarItemComanda(string baseUri)
            {
                return $"{baseUri}/api/comanda/item/adicionar";
            }

            public static string AtualizarItemComanda(string baseUri)
            {
                return $"{baseUri}/api/comanda/item/atualizar";
            }

            public static string RemoverItemComanda(string baseUri)
            {
                return $"{baseUri}/api/comanda/item/remover";
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

            public static string ObterTodos(string baseUri, int pagina, int itensPagina, int? filtroMarca, int? filtroCategoria)
            {
                var filtros = "";

                if (filtroCategoria.HasValue)
                {
                    var filtroMarcaQs = (filtroMarca.HasValue) ? filtroMarca.Value.ToString() : string.Empty;
                    filtros = $"/categoria/{filtroCategoria.Value}/marca/{filtroMarcaQs}";

                }
                else if (filtroMarca.HasValue)
                {
                    var filtroMarcaQs = (filtroMarca.HasValue) ? filtroMarca.Value.ToString() : string.Empty;
                    filtros = $"/categoria/marca/{filtroMarcaQs}";
                }
                else
                {
                    filtros = string.Empty;
                }


                //TODO: Criar API para filtro de marcas e categorias
                filtros = "";

                return $"{baseUri}/api/produto{filtros}/vitrine?pageIndex={pagina}&pageSize={itensPagina}";
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

        public static class Mesa
        {
            public static string ObterPorId(string baseUri, Guid id)
            {
                return $"{baseUri}/api​/mesa​/obter​/detalhe/{id}";
            }

            public static string ObterTodos(string baseUri)
            {
                return $"{baseUri}/api/mesa/obter/todos";
            }
        }

        public static class Usuario
        {
            public static string Autenticar(string baseUri)
            {
                return $"{baseUri}/api/conta/entrar";
            }
        }

    }
}