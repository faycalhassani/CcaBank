using System;

namespace CcaBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
            Console.ReadKey();
        }

        static void Start()
        {
            // initialiser les structure de donnees et les donnees
            // initialiser un tableau pour la liste des comptes
            // nextAccount sera utilisee pour identifier la position ou il faudra rajouter le prochain compte dans le tableau
            Account[] tabAccounts = new Account[10];
            int nextAccount = InitializeAccounts(tabAccounts);

            Console.WriteLine("Bienvenue a CCA BANK");
            // exit sera utilisee pour savoir si l'utilisateur a demande de sortir de l'applicaton
            bool exit = false;

            // tant que l'utilisateur n'a pas demande de sortir de l'pplication => la boucle va continuer a tourner
            while (!exit)
            {
                // obtenir le numero de compte de l'utilisateur
                // si Getaccount renvoie -1 => sortir
                int currentAccount = GetAccount(tabAccounts, nextAccount);
                if (currentAccount == -1)
                {
                    break;
                }

                // apres avoir trouve le numero de compte valide, obtenir le code pin de l'utilisateur pour ce compte
                // si Authenticate renvoie 'false' => sortir
                if (!Authenticate(tabAccounts, currentAccount))
                {
                    break;
                }

                // apres avoir authentifie l'utilisateur avec un numero de compte valid et un code pin : 
                // l'utilisateur doit etre capable d'effectuer plusieurs operation
                // tant que l'utilisateur n'a pas demande de sortir, 
                // l'application doit lui afficher le menu et lui proposer de selectionner une operation a effectuer
                while (!exit)
                {
                    // afficher menu
                    Menu();

                    // effectuer l'operation selectionnee par l'utilisateur
                    // si exit = true, sortir
                    exit = RunOperations(tabAccounts, currentAccount, exit);
                }
            }

            Console.WriteLine("Merci d'avoir utilise CCA BANK");
        }

        /// <summary>
        /// Initialiser une liste de comptes, et renvoyer le nombre de compte dans la liste
        /// </summary>
        /// <param name="tabAccounts">Liste de comptes vide</param>
        /// <returns>Le nombre de compte initialise dans la liste</returns>
        static int InitializeAccounts(Account[] tabAccounts)
        {
            Account acc0 = new Account();
            acc0.Number = 100;
            acc0.Pin = 1234;
            acc0.ClientName = "George Cloony";

            tabAccounts[0] = acc0;
            tabAccounts[1] = new Account { Number = 200, Pin = 2345, ClientName = "Jon Machin" };
            tabAccounts[2] = new Account { Number = 300, Pin = 3456, ClientName = "Forest Moi" };
            return 3;
        }

        /// <summary>
        /// Obtenir le numero de compte de l'utilisateur
        /// </summary>
        /// <param name="tabAccounts"></param>
        /// <param name="nextAccount"></param>
        /// <returns></returns>
        static int GetAccount(Account[] tabAccounts, int nextAccount)
        {
            // tant que le numero de compte n'est pas valide et l'utilisateur n'a pas demande de sortir, la boucle doit continuer a tourner
            while (true)
            {
                // obtenir le numero de compte
                Console.Write("Votre Numero de Compte : ");
                string tempAccountNumber = Console.ReadLine();
                // l'utilisateur peut demander a tout momemnt de sortir de l'application en saisissant 0 ou exit
                if (tempAccountNumber == "0" || tempAccountNumber == "exit")
                {
                    return -1;
                }
                // convertir le numero de compte de string a int
                int currentAccountNumber = Convert.ToInt32(tempAccountNumber);

                int currentAccount = -1;
                // parcourir le tableau pour trouver le numero de compte
                for (int i = 0; i < nextAccount; i++)
                {
                    if (tabAccounts[i].Number == currentAccountNumber)
                    {
                        currentAccount = i;
                        break;
                    }
                }

                // si le compte n'existe pas dans le tableau => afficher message d'erreur et recommencer la boucle pour demander encore le numero de compte
                if (currentAccount == -1)
                {
                    Console.WriteLine("Compte introuvable!");
                    continue;
                }
                return currentAccount;
            }
        }

        /// <summary>
        /// Obtenir le code pin de l'utilisateur et verifier que c'est le meme code attache au compte
        /// </summary>
        /// <param name="tabAccounts"></param>
        /// <param name="currentAccount"></param>
        /// <returns></returns>
        static bool Authenticate(Account[] tabAccounts, int currentAccount)
        {
            // tant que le code pin n'est pas valide et l'utilisateur n'a pas demande de sortir, la boucle doit continuer a tourner
            while (true)
            {
                // obtenir le code pin
                Console.Write("Votre Code PIN : ");
                string tempPin = Console.ReadLine();
                // l'utilisateur peut demander a tout momemnt de sortir de l'application en saisissant 0 ou exit
                if (tempPin == "0" || tempPin == "exit")
                {
                    return false;
                }

                // convertir le code pin de string a int
                int currentPin = Convert.ToInt32(tempPin);
                // si le code pin n'est pas le meme que celui du compte => recommencer la boucle pour demander encore le code pin
                if (tabAccounts[currentAccount].Pin != currentPin)
                {
                    Console.WriteLine("Code PIN Invalide");
                    continue;
                }
                return true;
            }
        }

        /// <summary>
        /// Afficher le menu
        /// </summary>
        static void Menu()
        {
            Console.WriteLine("Operations disponible :");
            Console.WriteLine("0- Sortie");
            Console.WriteLine("1- Retrait");
            Console.WriteLine("2- Depot");
            Console.WriteLine("3- Solde de compte");
            Console.Write("Veuillez saisir le numero de l'operation a effectuer: ");
        }

        /// <summary>
        /// Executer l'operation selectionner par l'utilisateur
        /// </summary>
        /// <param name="tabAccounts"></param>
        /// <param name="currentAccount"></param>
        /// <param name="exit"></param>
        /// <returns></returns>
        static bool RunOperations(Account[] tabAccounts, int currentAccount, bool exit)
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

        /// <summary>
        /// Effectuer un retrait d'un compte
        /// </summary>
        /// <param name="tabAccounts"></param>
        /// <param name="currentAccount"></param>
        /// <param name="exit"></param>
        /// <returns></returns>
        static bool Retrait(Account[] tabAccounts, int currentAccount, bool exit)
        {
            // tant que le retrait n'est pas reussi et l'utilisateur n'a pas demande de sortir, la boucle doit continuer a tourner
            while (true)
            {
                // obtenir le montant a retrer
                Console.Write("Montant a retirer : ");
                string tempAmount = Console.ReadLine();
                // l'utilisateur peut demander a tout momemnt de sortir de l'application en saisissant 0 ou exit
                if (tempAmount == "0" || tempAmount == "exit")
                {
                    exit = true;
                    break;
                }

                // convertr le montant de string a int
                int amount = Convert.ToInt32(tempAmount);

                // effectuer le retrait avec l'objet Account
                if(!tabAccounts[currentAccount].Retrait(amount))
                {
                    Console.WriteLine("Le montant a retirer doit etre superieur a 0 et inferiuer a votre solde : " + tabAccounts[currentAccount].Balance);
                    continue;
                }
                
                Console.WriteLine("Retrait complete");
                Console.WriteLine("Nouveau solde : " + tabAccounts[currentAccount].Balance);
                break;
            }
            return exit;
        }

        /// <summary>
        /// Effectuer un depot dans un compte
        /// </summary>
        /// <param name="tabAccounts"></param>
        /// <param name="currentAccount"></param>
        /// <param name="exit"></param>
        /// <returns></returns>
        static bool Depot(Account[] tabAccounts, int currentAccount, bool exit)
        {
            // tant que le depot n'est pas reussi et l'utilisateur n'a pas demande de sortir, la boucle doit continuer a tourner
            while (true)
            {
                // obtenir le montant a deposer
                Console.Write("Montant a depositer : ");
                string tempAmount = Console.ReadLine();
                // l'utilisateur peut demander a tout momemnt de sortir de l'application en saisissant 0 ou exit
                if (tempAmount == "0" || tempAmount == "exit")
                {
                    exit = true;
                    break;
                }

                // convertir le montant a deposer de string a int
                int amount = Convert.ToInt32(tempAmount);
                // effectuer le depot avec l'objet Account
                if (!tabAccounts[currentAccount].Depot(amount))
                {
                    Console.WriteLine("Le montant a deposer doit etre superieur a 0");
                    continue;
                }
                
                Console.WriteLine("Depot complete");
                Console.WriteLine("Nouveau solde : " + tabAccounts[currentAccount].Balance);
                break;
            }
            return exit;
        }

        
    }
}
