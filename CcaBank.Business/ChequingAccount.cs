using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcaBank.Business
{
    public class ChequingAccount : Account
    {
        public int MonthlyFees { get; set; }

        public override int GetBalance()
        {
            return Balance;
        }
    }
}
