
namespace CcaBank.Business
{
    public class SavingAccount : Account
    {
        public double InterestRate { get; set; }

        public SavingAccount(double interestRate)
        {
            InterestRate = interestRate;
        }

        /// <summary>
        /// Obtenr le solde du compte
        /// override = cette fonction ecrase le comportement de la meme fonction definie au niveau du parent
        /// </summary>
        /// <returns></returns>
        public override int GetBalance()
        {
            return Balance + (int)(Balance * InterestRate);
        }
    }
}
