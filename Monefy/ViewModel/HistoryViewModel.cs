using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Model;
using Monefy.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModel
{
    public class HistoryViewModel : ViewModelBase
    {
        private readonly IMessenger Messenger;
        private readonly IUserDataService UserDataService;

        public HistoryViewModel(IMessenger messenger, IUserDataService userDataService)
        {
            Messenger = messenger;
            UserDataService = userDataService;

            Messenger.Register<UpdateUserDataMessage>(this, message =>
            {
                if (UserDataService.Data!.PurchaseHistory != null)
                    PurchaseRecords = new(UserDataService.Data.PurchaseHistory);
            });

            if (UserDataService.Data!.PurchaseHistory != null)
                PurchaseRecords = new(UserDataService.Data.PurchaseHistory);
        }

        public ObservableCollection<PurchaseRecord> PurchaseRecords { get; set; } = new();
    }
}
