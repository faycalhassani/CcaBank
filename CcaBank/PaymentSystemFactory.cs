using CcaBank.Business;

namespace CcaBank
{
    class PaymentSystemFactory
    {
        public static IPaymentSystem Get(int party)
        {
            switch (party)
            {
                case 1: return new WirePaymentSystem(party);
                case 2: return new VisaPaymentSystem(party);
                default: return new WirePaymentSystem(party);
            }
        }
    }
}
