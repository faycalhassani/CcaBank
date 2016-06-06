using CcaBank.Business;
using System.Collections.Generic;

namespace CcaBank.Data
{
    /// <summary>
    /// Classe qui contient une representation des structures de donnees
    /// Les donnees peuvent tre en memoire en dans une base de donnees
    /// Un Data context est un ensemble de data sets
    /// </summary>
    class DataContext
    {
        /// <summary>
        /// Le constructeur du data context initialise votre structure en memoire 
        /// ou initialise la connection a la base de donnees si les donnees sont dans une base
        /// </summary>
        public DataContext()
        {
            Accounts = new Dictionary<int, Account>();
            var acc0 = new SavingAccount(0.1);
            acc0.Number = 100;
            acc0.Pin = 1234;
            acc0.ClientName = "George Cloony";

            Accounts.Add(acc0.Number, acc0);
            Accounts.Add(200, new ChequingAccount() { Number = 200, Pin = 2345, ClientName = "Jon Machin" });
            Accounts.Add(300, new SavingAccount(0.09) { Number = 300, Pin = 3456, ClientName = "Forest Moi" });
        }

        // data sets
        public Dictionary<int, Account> Accounts { get; set; }
    }
}
