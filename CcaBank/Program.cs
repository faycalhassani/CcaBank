using System;

namespace CcaBank
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize structure and data
            Account[] tabAccounts = new Account[10];
            int nextAccount = InitializeAccounts(tabAccounts);

            Operation[] tabOperations = new Operation[100];
            int nextOperation = InitializeOperations(tabOperations);

            Console.WriteLine("Bienvenue a CCA BANK");
            bool exit = false;
            while (!exit)
            {
                // authentication
                int currentAccount = GetAccount(tabAccounts, nextAccount);
                if (currentAccount == -1)
                {
                    break;
                }

                if (!Authenticate(tabAccounts, currentAccount))
	            {
                    break;
	            }

                // operations
                while (!exit)
                {
                    // menu
                    Menu();

                    // run operations
                    exit = RunOperations(tabAccounts, currentAccount, exit);
                }
            }

            Console.WriteLine("Merci d'avoir utilise CCA BANK");
            Console.ReadKey();
        }

        private static bool RunOperations(Account[] tabAccounts, int currentAccount, bool exit)
        {
            string tempOperation = Console.ReadLine();
            switch (tempOperation)
            {
                // exit
                case "0":
                    exit = true;
                    break;

                case "exit":
                    exit = true;
                    break;

                // retrait
                case "1":
                    exit = Retrait(tabAccounts, currentAccount, exit);
                    break;

                // depot
                case "2":
                    exit = Depot(tabAccounts, currentAccount, exit);
                    break;

                // solde
                case "3":
                    Console.WriteLine("Votres solde : " + tabAccounts[currentAccount].Balance);
                    break;

                // autre
                default:
                    Console.WriteLine("Operation invalide");
                    break;
            }
            return exit;
        }

        static bool Authenticate(Account[] tabAccounts, int currentAccount)
        {
            while (true)
            {
                Console.Write("Votre Code PIN : ");
                string tempPin = Console.ReadLine();
                if (tempPin == "0" || tempPin == "exit")
                {
                    return false;
                }

                int currentPin = Convert.ToInt32(tempPin);
                if (tabAccounts[currentAccount].Pin != currentPin)
                {
                    Console.WriteLine("Code PIN Invalide");
                    continue;
                }
                return true;
            }
        }

        private static int GetAccount(Account[] tabAccounts, int nextAccount)
        {
            while (true)
            {
                Console.Write("Votre Numero de Compte : ");
                string tempAccountNumber = Console.ReadLine();
                if (tempAccountNumber == "0" || tempAccountNumber == "exit")
                {
                    return -1;
                }
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
                return currentAccount;
            }
        }

        static int InitializeAccounts(Account[] tabAccounts)
        {
            tabAccounts[0] = new Account { Number = 100, Pin = 1234, Balance = 2000, ClientName = "George Cloony" };
            tabAccounts[1] = new Account { Number = 200, Pin = 2345, Balance = 3000, ClientName = "Jon Machin" };
            tabAccounts[2] = new Account { Number = 300, Pin = 3456, Balance = 100, ClientName = "Forest Moi" };
            return 3;
        }

        static int InitializeOperations(Operation[] tabOperations)
        {
            return 0;
        }

        static bool Depot(Account[] tabAccounts, int currentAccount, bool exit)
        {
            while (true)
            {
                Console.Write("Montant a depositer : ");
                string tempAmount = Console.ReadLine();
                if (tempAmount == "0" || tempAmount == "exit")
                {
                    exit = true;
                    break;
                }

                int amount = Convert.ToInt32(tempAmount);
                if (amount <= 0)
                {
                    Console.WriteLine("Le montant a deposer doit etre superieur a 0");
                    continue;
                }

                tabAccounts[currentAccount].Balance += amount;
                Console.WriteLine("Depot complete");
                break;
            }
            return exit;
        }

        static bool Retrait(Account[] tabAccounts, int currentAccount, bool exit)
        {
            while (true)
            {
                Console.Write("Montant a retirer : ");
                string tempAmount = Console.ReadLine();
                if (tempAmount == "0" || tempAmount == "exit")
                {
                    exit = true;
                    break;
                }

                int amount = Convert.ToInt32(tempAmount);
                if (amount <= 0)
                {
                    Console.WriteLine("Le montant a retirer doit etre superieur a 0");
                    continue;
                }
                if (amount > tabAccounts[currentAccount].Balance)
                {
                    Console.WriteLine("Solde insuffisant : " + tabAccounts[currentAccount].Balance);
                    continue;
                }
                tabAccounts[currentAccount].Balance -= amount;
                Console.WriteLine("Retrait complete");
                break;
            }
            return exit;
        }

        static void Menu()
        {
            Console.WriteLine("Operations disponible :");
            Console.WriteLine("0- Sortie");
            Console.WriteLine("1- Retrait");
            Console.WriteLine("2- Depot");
            Console.WriteLine("3- Solde de compte");
            Console.Write("Veuillez saisir le numero de l'operation a effectuer: ");
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
