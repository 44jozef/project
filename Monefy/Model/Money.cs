using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Model
{
    public class Money
    {
        private float _amount = 0.0f;
        public float Amount
        {
            get => _amount;
            set
            {
                _amount = RoundToCent(value);
            }
        }

        public Money(float amount = 0.0f)
        {
            Amount = amount;
        }

        public static float RoundToCent(float num)
        {
            return ((float)Math.Round((Decimal)num, 2, MidpointRounding.AwayFromZero));
        }
    }
}
