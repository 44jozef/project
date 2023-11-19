using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModel
{
    public class MonefyViewModel : ViewModelBase
    {
        public static ObservableCollection<string> Categories { get; set; } = new();

        private readonly IMessenger Messenger;
        private readonly INavigationService NavigationService;

        public MonefyViewModel(IMessenger messenger, INavigationService navigationService)
        {
            Messenger = messenger;
            NavigationService = navigationService;

            Messenger.Register<NavigationMessage>(this, message =>
            {
                var viewModel = App.Container.GetInstance(message.ViewModelType) as ViewModelBase;
                CurrentViewModel = viewModel;
            });

            NavigationService.NavigateTo<GeneralViewModel>();
        }

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => Set(ref _currentViewModel, value);
        }

        private RelayCommand? _generalNavCommand;
        public RelayCommand GeneralNavCommand 
        { 
            get => _generalNavCommand ??= new RelayCommand(() =>
            {
                NavigationService.NavigateTo<GeneralViewModel>();
            }); 
        }

        private RelayCommand? _historyNavCommand;
        public RelayCommand HistoryNavCommand 
        { 
            get => _historyNavCommand ??= new RelayCommand(() =>
            {
                NavigationService.NavigateTo<HistoryViewModel>();
            }); 
        }
    }
}
