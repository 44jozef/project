using LiveCharts;
using Monefy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services
{
    public interface IExpensesStatsService
    {
        SeriesCollection ComputePieChartData(UserData userData);
    }
}
