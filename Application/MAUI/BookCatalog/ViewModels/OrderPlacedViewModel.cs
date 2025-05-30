using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;

namespace BookCatalog.ViewModels
{
    public class OrderPlacedViewModel : BaseViewModel
    {
        public IRelayCommand CloseCommand { get; }
        public event Action? CloseRequested;

        public OrderPlacedViewModel()
        {
            CloseCommand = new RelayCommand(OnClose);
        }

        private void OnClose()
        {
            CloseRequested?.Invoke();
        }
    }
}
