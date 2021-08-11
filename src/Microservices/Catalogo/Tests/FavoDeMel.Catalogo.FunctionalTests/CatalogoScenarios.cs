using FavoDeMel.Catalogo.Application.ViewModels;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Catalogo.FunctionalTests
{
    public class CatalogoScenarios : CatalogoScenarioBase
    {
        [Fact]
        public async Task Get_get_all_account_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.Accounts);

                response.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Cancel_order_no_order_created_bad_request_response()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(BuildAccount(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateIdempotentClient()
                    .PutAsync(Put.CancelOrder, content);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task Ship_order_no_order_created_bad_request_response()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(BuildAccount(), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateIdempotentClient()
                    .PutAsync(Put.ShipOrder, content);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        string BuildAccount()
        {
            var produto = new ProdutoViewModel()
            {
                //TODO:
            };
            return JsonSerializer.Serialize(produto);
        }
    }
}
