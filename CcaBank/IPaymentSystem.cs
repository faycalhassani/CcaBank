using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcaBank
{
    interface IPaymentSystem
    {
        bool Pay(int amount);
    }
}
