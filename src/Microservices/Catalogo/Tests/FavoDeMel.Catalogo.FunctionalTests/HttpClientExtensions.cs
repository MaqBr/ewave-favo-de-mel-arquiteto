using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.FunctionalTests
{
    static class HttpClientExtensions
    {
        public static HttpClient CreateIdempotentClient(this TestServer server)
        {
            var client = server.CreateClient();
            client.DefaultRequestHeaders.Add("x-requestid", Guid.NewGuid().ToString());
            return client;
        }
    }
}
