using CcaBank.Business;
using System.Collections.Generic;

namespace CcaBank.Payment
{
    class PaymentSystemFactory
    {
        private Dictionary<int, IPaymentSystem> _paymentSystems;

        public PaymentSystemFactory()
        {
            _paymentSystems = new Dictionary<int, IPaymentSystem>();
            Register(1, new WirePaymentSystem(1));
            Register(2, new VisaPaymentSystem(2));
            Register(3, new VisaPaymentSystem(3));
        }

        public void Register(int party, IPaymentSystem paymentSystem)
        {
            _paymentSystems.Add(party, paymentSystem);
        }

        public IPaymentSystem Get(int party)
        {
            if (_paymentSystems.ContainsKey(party))
            {
                return _paymentSystems[party];
            }
            return new WirePaymentSystem(party);
        }
    }
}
