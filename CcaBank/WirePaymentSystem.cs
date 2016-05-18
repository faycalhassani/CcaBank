using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcaBank
{
    class WirePaymentSystem : IPaymentSystem
    {
        public int Party { get; set; }
        public WirePaymentSystem(int party)
        {
            Party = party;
        }

        public bool Pay(int amount)
        {
            // code here
            return true;
        }
    }
}
