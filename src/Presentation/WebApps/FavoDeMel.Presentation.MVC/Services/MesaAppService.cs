using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FavoDeMel.Domain.Core.Extensions;
using FavoDeMel.Domain.Core.Model.Configuration;
using FavoDeMel.Presentation.MVC.CatalogoViewModels.ViewModels;
using FavoDeMel.Presentation.MVC.Models.DTO;
using FavoDeMel.Presentation.MVC.ViewModels;
using FavoDeMel.Presentation.MVC.ViewModels.MesaViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebMVC.Infrastructure;

namespace FavoDeMel.Presentation.MVC.Services
{
    public class MesaAppService : IMesaAppService
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MesaAppService> _logger;
        private IConfiguration _configuration;
        private readonly string _remoteServiceBaseUrl;

        public MesaAppService(HttpClient httpClient, 
            ILogger<MesaAppService> logger,
            IConfiguration configuration)
        {

            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;
            _appSettings = configuration.GetAppSettings();
            _remoteServiceBaseUrl = $"{_appSettings.Microservices.CatalogoBaseUrl}";
        }


        public async Task<IEnumerable<MesaViewModel>> ObterTodos()
        {

            //Mock
            var mesas = new List<MesaViewModel>
            {
                new MesaViewModel { MesaId = Guid.NewGuid(), Numero = 1, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new MesaViewModel { MesaId = Guid.NewGuid(), Numero = 2, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new MesaViewModel { MesaId = Guid.NewGuid(), Numero = 3, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new MesaViewModel { MesaId = Guid.NewGuid(), Numero = 4, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new MesaViewModel { MesaId = Guid.NewGuid(), Numero = 5, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new MesaViewModel { MesaId = Guid.NewGuid(), Numero = 6, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new MesaViewModel { MesaId = Guid.NewGuid(), Numero = 7, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new MesaViewModel { MesaId = Guid.NewGuid(), Numero = 8, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new MesaViewModel { MesaId = Guid.NewGuid(), Numero = 9, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre },
                new MesaViewModel { MesaId = Guid.NewGuid(), Numero = 10, DataCriacao = DateTime.Now, Situacao = SituacaoMesa.Livre }

            };

            return mesas;
        }

        public async Task<MesaViewModel> ObterPorId(Guid id)
        {
            //Mock
            var mesa = await ObterTodos();
            var resultado = mesa.FirstOrDefault();
            return resultado;
        }

    }
}