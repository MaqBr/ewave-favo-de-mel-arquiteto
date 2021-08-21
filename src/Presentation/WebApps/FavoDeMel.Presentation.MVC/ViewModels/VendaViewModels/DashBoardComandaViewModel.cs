using System;
using System.Collections.Generic;
using FavoDeMel.Presentation.MVC.ViewModels;

namespace FavoDeMel.Presentation.MVC.Venda.ViewModels
{
    public class DashBoardComandaViewModel
    {
        public DashBoardComandaViewModel()
        {
            Data = new List<ComandaViewModel>();
        }
        public ComandaStatus Status { get; set; }
        public IEnumerable<ComandaViewModel> Data { get; set; }
    }

}