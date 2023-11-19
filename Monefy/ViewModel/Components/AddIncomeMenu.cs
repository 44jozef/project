using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using Monefy.Messages;
using Monefy.Model;
using Monefy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModel.Components
{
    public class AddIncomeMenu : ViewModelBase
    {
        private readonly IMessenger Messenger;
        private readonly IUserDataService UserDataService;

        public AddIncomeMenu(IMessenger messenger, IUserDataService userDataService)
        {
            Messenger = messenger;
            UserDataService = userDataService;
        }

        private Money MoneyInp = new();

        private string _amountText = "";
        public string AmountText
        {
            get => _amountText;
            set
            {
                string prevAmountText = _amountText;

                /**
                 * Instant validation logic
                 */
                float tmp = 0.0f;
                bool isfloat = float.TryParse(value, out tmp);
                if (isfloat || string.IsNullOrEmpty(value))
                {
                    MoneyInp.Amount = tmp;
                    Set(ref _amountText, value);
                    if (prevAmountText != value)
                        ValidateButton();
                }
            }
        }

        private bool _addEnabled = false;
        public bool AddEnabled
        {
            get => _addEnabled;
            set => Set(ref _addEnabled, value);
        }

        private RelayCommand? _addIncomeCommand;
        public RelayCommand AddIncomeCommand
        {
            get => _addIncomeCommand ??= new RelayCommand(() =>
            {
                UserDataService.YieldBalance(MoneyInp.Amount);
                UserDataService.RecordPurchase(new() { Category = new() { Name = "Income" }, CategoryType = ECategoryType.Income, Value = MoneyInp.Amount });
                DialogHost.Close("RootDialog");

                Messenger.Send<UpdateUserDataMessage>(new());
            });
        }

        private void ValidateButton()
        {
            AddEnabled = MoneyInp.Amount != 0.0f;
        }
    }
}
