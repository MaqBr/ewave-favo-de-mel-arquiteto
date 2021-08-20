using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalogo.FunctionalTests
{
    public class CatalogoScenarios : CatalogoScenarioBase
    {
        [Fact]
        [Trait("Produto", "API - Obter todos os produtos")]
        public async Task Obter_todos_produtos_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.ObterProdutos);

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        [Trait("Produto", "API - Obter todos os produtos paginados")]
        public async Task Obter_todos_produtos_paginacao_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.ObterProdutosPaginado(10,0));

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        [Trait("Produto", "API - Obter todos os produtos paginados")]
        public async Task Obter_produto_por_id_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.ObterProdutoDetalhe(new Guid("7037bfcd-b7b2-4371-8f1f-445e0f89df38")));

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        [Trait("Categoria", "API - Obter todos as categorias")]
        public async Task Obter_categorias_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.ObterCategorias);

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        [Trait("Produto", "API - Obter todos os produtos por categoria")]
        public async Task Obter_Produtos_por_categoria_id_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.ObterProdutosPorCategoria(102));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
