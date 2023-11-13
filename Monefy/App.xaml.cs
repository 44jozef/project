using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Services;
using Monefy.View;
using Monefy.ViewModel;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Monefy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Register();

            StartMain<MonefyViewModel>();

            base.OnStartup(e);
        }

        public void Register()
        {
            Container = new Container();

            Container.RegisterSingleton<INavigationService, NavigationService>();
            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<IUserDataService, UserDataService>();
            Container.RegisterSingleton<IExpensesStatsService, ExpensesStatsService>();
            
            Container.RegisterSingleton<MonefyViewModel>();
            Container.RegisterSingleton<GeneralViewModel>();
            Container.RegisterSingleton<HistoryViewModel>();

            Container.Verify();
        }

        public void StartMain<Ty>() where Ty : ViewModelBase
        {
            Window window = new MonefyView();
            var viewModel = Container?.GetInstance<Ty>();

            window.DataContext = viewModel;

            window.ShowDialog();
        }
    }
}
