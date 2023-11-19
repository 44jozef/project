using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
using Monefy.Messages;
using Monefy.Model;
using Monefy.Services;
using Monefy.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModel
{
    public class GeneralViewModel : ViewModelBase
    {
        private readonly IMessenger Messenger;
        private readonly IUserDataService UserDataService;
        private readonly IExpensesStatsService ExpensesStatsService;

        public GeneralViewModel(IMessenger messenger, IUserDataService userDataService, IExpensesStatsService expensesStatsService)
        {
            Messenger = messenger;
            UserDataService = userDataService;
            ExpensesStatsService = expensesStatsService;

            Update();

            Messenger.Register<UpdateUserDataMessage>(this, message => Update());
            Messenger.Register<NavigationMessage>(this, message => Update());
        }

        private SeriesCollection _expensesStatsCollection;
        public SeriesCollection ExpensesStatsCollection
        {
            get => _expensesStatsCollection;
            set => Set(ref _expensesStatsCollection, value);
        }

        private float _balance;
        public float Balance
        {
            get => _balance;
            set => Set(ref _balance, value);
        }

        private RelayCommand? _openIncomeDialogCommand;
        public RelayCommand OpenIncomeDialogCommand
        {
            get => _openIncomeDialogCommand ??= new RelayCommand(async () =>
            {
                await DialogHost.Show(new AddIncomeMenu(Messenger, UserDataService), "RootDialog");
            });
        }

        private RelayCommand? _openExpenseDialogCommand;
        public RelayCommand OpenExpenseDialogCommand
        {
            get => _openExpenseDialogCommand ??= new RelayCommand(async () =>
            {
                await DialogHost.Show(new AddExpenseMenu(Messenger, UserDataService), "RootDialog");
            });
        }

        private void Update()
        {
            Balance = UserDataService.Data.Balance;
            ExpensesStatsCollection = ExpensesStatsService.ComputePieChartData(UserDataService.Data);
        }
    }
}
