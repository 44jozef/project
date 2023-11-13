using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Monefy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services
{
    public class ExpensesStatsService : IExpensesStatsService
    {
        public SeriesCollection ComputePieChartData(UserData userData)
        {
            SeriesCollection pieChartData = new();
            Dictionary<string, float> categExpensesMap = new();

            // For pie chart to have consistent markings
            foreach (var category in userData.Categories)
            {
                categExpensesMap.Add(category.Name, 0.0f);
            }

            if (userData.PurchaseHistory != null)
            {
                foreach (var record in userData.PurchaseHistory)
                {
                    if (record.CategoryType == ECategoryType.Expense)
                    {
                        categExpensesMap[record.Category.Name] += record.Value;
                    }
                }
            }

            foreach (var item in categExpensesMap)
            {
                pieChartData.Add(new PieSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<ObservableValue> { new ObservableValue((double)Math.Round((Decimal)item.Value, 2, MidpointRounding.AwayFromZero)) },
                    DataLabels = true
                });
            }

            return pieChartData;
        }
    }
}
