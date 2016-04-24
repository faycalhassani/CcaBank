using System;

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
            bool exit = false;
            while (!exit)
            {
                // authentication
                Console.Write("Votre Numero de Compte : ");
                string tempAccountNumber = Console.ReadLine();
                if (tempAccountNumber == "0" || tempAccountNumber == "exit")
                {
                    break;
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

                while (!exit)
                {
                    Console.Write("Votre Code PIN : ");
                    string tempPin = Console.ReadLine();
                    if (tempPin == "0" || tempPin == "exit")
                    {
                        exit = true;
                        break;
                    }

                    int currentPin = Convert.ToInt32(tempPin);
                    if (tabAccounts[currentAccount].Pin != currentPin)
                    {
                        Console.WriteLine("Code PIN Invalide");
                        continue;
                    }
                    break;
                }
                
                // operations
                while (!exit)
                {
                    // menu
                    Console.WriteLine("Operations disponible :");
                    Console.WriteLine("0- Sortie");
                    Console.WriteLine("1- Retrait");
                    Console.WriteLine("2- Depot");
                    Console.WriteLine("3- Solde de compte");
                    Console.Write("Veuillez saisir le numero de l'operation a effectuer: ");

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
                            break;
                        
                        // depot
                        case "2":
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
                }
            }

            Console.WriteLine("Merci d'avoir utilise CCA BANK");
            Console.ReadKey();
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
