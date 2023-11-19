using Monefy.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Monefy.Services
{
    public class UserDataService : IUserDataService
    {
        public string Filepath { get; set; } = "UserData.json";

        private UserData? _data;
        public UserData? Data
        {
            get => _data;
            set => Update(value);
        }

        public UserDataService()
        {
            Data = Fetch();   
            if (Data == null)
            {
                CategoryModel[] defaultCategories = new CategoryModel[]{ 
                    new() { Name="Food", IconPath="FoodForkDrink" },
                    new() { Name="Commuting", IconPath="TrainCarPassenger" },
                    new() { Name="House", IconPath="Home" },
                    new() { Name="Entertainment", IconPath="GamepadSquare" }
                };

                Data = new() { Balance = 0.0f, Categories = defaultCategories };
            }
        }

        public void YieldBalance(float amount)
        {
            Data.Balance = Money.RoundToCent(Data.Balance + amount);
            Update(Data);
        }

        public void RecordPurchase(PurchaseRecord purchaseRecord)
        {
            if (Data.PurchaseHistory != null)
            {
                List<PurchaseRecord> purchaseRecords = new(Data.PurchaseHistory);
                purchaseRecords.Insert(0, purchaseRecord);
                Data.PurchaseHistory = purchaseRecords.ToArray();
            }
            else
            {
                Data.PurchaseHistory = new PurchaseRecord[] { purchaseRecord };
            }

            Update(Data);
        }

        private void Update(UserData? userData)
        {
            using FileStream fs = new(Filepath, FileMode.Create);

            JsonSerializer.Serialize(fs, userData);

            _data = userData;
        }

        private UserData? Fetch()
        {
            using FileStream fs = new(Filepath, FileMode.OpenOrCreate);

            UserData? userData = null;

            try
            {
                userData = JsonSerializer.Deserialize<UserData>(fs);
            }
            catch (JsonException) { }

            return userData;
        }

    }
}
