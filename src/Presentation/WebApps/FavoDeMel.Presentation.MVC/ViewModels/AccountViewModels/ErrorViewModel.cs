using System;

namespace FavoDeMel.Presentation.MVC.ViewModels.AccountViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}