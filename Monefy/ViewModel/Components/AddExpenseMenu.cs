using GalaSoft.MvvmLight.Command;
using Monefy.Model;
using Monefy.Services;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monefy.Messages;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;

namespace Monefy.ViewModel.Components
{
    public class AddExpenseMenu : ViewModelBase
    {
        private readonly IMessenger Messenger;
        private readonly IUserDataService UserDataService;

        public AddExpenseMenu(IMessenger messenger, IUserDataService userDataService)
        {
            Messenger = messenger;
            UserDataService = userDataService;

            Categories = new(UserDataService.Data.Categories);
        }

        public List<CategoryModel>? Categories { get; init; }
        public CategoryModel? SelectedCategory { get; set; }

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
                        ValidateButtonCommand.Execute(_validateButtonCommand);
                }
            } 
        }

        private bool _addEnabled = false;
        public bool AddEnabled
        {
            get => _addEnabled;
            set => Set(ref _addEnabled, value);
        }

        private RelayCommand? _addExpenseCommand;
        public RelayCommand AddExpenseCommand
        {
            get => _addExpenseCommand ??= new RelayCommand(() =>
            {
                UserDataService.YieldBalance(-1 * MoneyInp.Amount);
                UserDataService.RecordPurchase(new() { Category = SelectedCategory, CategoryType = ECategoryType.Expense, Value = MoneyInp.Amount });
                DialogHost.Close("RootDialog");

                Messenger.Send<UpdateUserDataMessage>(new());
            });
        }

        private RelayCommand? _validateButtonCommand;
        public RelayCommand ValidateButtonCommand
        {
            get => _validateButtonCommand ??= new RelayCommand(() =>
            {
                AddEnabled = SelectedCategory != null && MoneyInp.Amount != 0.0f;
            });
        }
    }
}
