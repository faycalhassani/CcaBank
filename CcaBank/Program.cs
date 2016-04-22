using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcaBank
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize structure and data
            Account[] tabAccounts = new Account[10];
            tabAccounts[0] = new Account { Number = 100, Pin = 1234, Balance = 2000, ClientName = "George Cloony" };
            tabAccounts[1] = new Account { Number = 200, Pin = 2345, Balance = 3000, ClientName = "Jon Machin" };
            tabAccounts[2] = new Account { Number = 300, Pin = 3456, Balance = 100, ClientName = "Forest Moi" };
            int nextAccount = 3;

            Operation[] tabOperations = new Operation[100];
            int nextOperation = 0;

            Console.WriteLine("Bienvenue a CCA BANK");

            while (true)
            {
                // authentication
                Console.Write("Votre Numero de Compte : ");
                string tempAccountNumber = Console.ReadLine();
                int currentAccountNumber = Convert.ToInt32(tempAccountNumber);

                int currentAccount = -1;
                for (int i = 0; i < nextAccount; i++)
                {
                    if (tabAccounts[i].Number == currentAccountNumber)
                    {
                        currentAccount = i;
                        break;
                    }
                }

                if (currentAccount == -1)
                {
                    Console.WriteLine("Compte introuvable!");
                    continue;
                }

                while (true)
                {
                    Console.Write("Votre Code PIN : ");
                    string tempPin = Console.ReadLine();
                    int currentPin = Convert.ToInt32(tempPin);
                    if (tabAccounts[currentAccount].Pin != currentPin)
                    {
                        Console.WriteLine("Code PIN Invalide");
                        continue;
                    }
                    break;
                }
                
                // menu

            }
            

        }
    }

    struct Account
    {
        public int Number;
        public int Pin;
        public int Balance;
        public string ClientName;
    }

    struct Operation
    {
        public int Number;
        public int AccountNumer;
        public int Amount;
        public DateTime Date;
    }
}
