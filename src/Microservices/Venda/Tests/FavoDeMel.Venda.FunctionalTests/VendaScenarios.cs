using System;
using System.Threading.Tasks;
using Xunit;

namespace Venda.FunctionalTests
{
    public class VendaScenarios : VendaScenarioBase
    {
        [Fact]
        [Trait("Venda", "API - Obter todas as mesas")]
        public async Task Obter_todas_mesas_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync(Get.ObterMesas());

                response.EnsureSuccessStatusCode();
            }
        }

    }
}
