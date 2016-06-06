
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
