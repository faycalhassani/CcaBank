﻿using CcaBank.Business;

namespace CcaBank
{
    class VisaPaymentSystem : IPaymentSystem
    {
        public int Party { get; set; }
        public VisaPaymentSystem(int party)
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
