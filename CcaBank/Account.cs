
using System;
namespace CcaBank
{
    /// <summary>
    /// Represente un compte bancaire
    /// </summary>
    class Account
    {
        public int Number { get; set; }
        public int Pin { get; set; }
        public int Balance { get; private set; }
        public string ClientName { get; set; }

        /// <summary>
        /// Liste des operations pour ce compte
        /// </summary>
        public Operation[] Operations { get; set; }
        private int _nextOperation;

        /// <summary>
        /// Constructeur de la class Account. 
        /// Il sert a initialiser des valeurs au moment de la createion d'un comte
        /// </summary>
        public Account()
        {
            Operations = new Operation[20];
            _nextOperation = 0;
        }

        /// <summary>
        /// Operation pour effectuer un depot dans le compte
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool Depot(int amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            Balance += amount;
            // ajouter le retrait a la lists des operations
            LogOperation(amount);
            return true;
        }

        /// <summary>
        /// Operation pour effectuer un retrait du compte
        /// </summary>
        /// <param name="amount">Montant a retirer. Doit etre superieur a zero (0) et inferieur au solde du compte</param>
        /// <returns></returns>
        public bool Retrait(int amount)
        {
            if (amount <= 0 || amount > Balance)
            {
                return false;
            }
            Balance -= amount;
            // ajouter le retrait a la lists des operations
            LogOperation(-amount);
            return true;
        }

        /// <summary>
        /// Ajouter une operation a la liste des peration et renvoyer le nombre d'operation dans la liste
        /// </summary>
        /// <param name="tabOperations"></param>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <param name="nextOperation"></param>
        private void LogOperation(int amount)
        {
            Operations[_nextOperation] = new Operation { AccountNumber = Number, Number = _nextOperation, Amount = amount, Date = DateTime.Now };
            _nextOperation++;
        }
    }
}
