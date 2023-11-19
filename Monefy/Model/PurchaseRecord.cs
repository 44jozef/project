using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Model
{
    public enum ECategoryType { Income, Expense }

    public class PurchaseRecord
    { 
        public CategoryModel? Category { get; set; }
        public ECategoryType CategoryType { get; set; }
        public float Value { get; set; }
    }
}
