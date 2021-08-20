using System.Threading.Tasks;
using Xunit;

namespace Catalogo.FunctionalTests
{
    public class CatalogoScenarios : CatalogoScenarioBase
    {
        [Fact]
        public async Task Obter_Todos_Produtos_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.ObterProdutos);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
