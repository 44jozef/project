using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Model
{
    public class UserData
    {
        public float Balance { get; set; }
        public PurchaseRecord[]? PurchaseHistory { get; set; }
        public CategoryModel[]? Categories { get; set; }
    }
}
