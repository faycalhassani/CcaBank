using CcaBank.Business;

namespace CcaBank.Payment
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
