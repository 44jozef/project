using Monefy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services
{
    public interface IUserDataService
    {
        public string Filepath { get; set; }

        public UserData? Data { get; set; }

        public void YieldBalance(float amount);
        public void RecordPurchase(PurchaseRecord purchaseRecord);
    }
}
