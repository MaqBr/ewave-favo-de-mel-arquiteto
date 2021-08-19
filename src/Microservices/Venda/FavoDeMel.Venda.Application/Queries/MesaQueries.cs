using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoDeMel.Venda.Application.Queries.ViewModels;
using FavoDeMel.Venda.Domain.Models;

namespace FavoDeMel.Venda.Application.Queries
{
    public class MesaQueries : IMesaQueries
    {
        private readonly IMesaRepository _mesaRepository;

        public MesaQueries(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public async Task<MesaViewModel> ObterPorId(Guid mesaId)
        {
            var mesa = await _mesaRepository.ObterPorId(mesaId);
            if (mesa == null) return null;

            var mesaVM = new MesaViewModel
            {
                MesaId = mesa.Id,
                Numero = mesa.Numero,
                DataCriacao = mesa.DataCriacao,
                Situacao = mesa.Situacao
            };

            return mesaVM;
        }

        public async Task<IEnumerable<MesaViewModel>> ObterTodos()
        {
            var mesas = await _mesaRepository.ObterTodos();


            if (!mesas.Any()) return null;

            var mesasView = new List<MesaViewModel>();

            foreach (var mesa in mesas)
            {
                mesasView.Add(new MesaViewModel
                {
                    MesaId = mesa.Id,
                    Numero = mesa.Numero,
                    DataCriacao = mesa.DataCriacao,
                    Situacao = mesa.Situacao
                });
            }

            return mesasView;
        }

    }
}