using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CcaBank.Business;

namespace CcaBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Files();
            //Collections();
            //Start();
            Console.ReadKey();
        }

        static void Start()
        {
            // initialiser le code de la banque
            Account.BankCode = "003";

            // initialiser les structure de donnees et les donnees
            // initialiser un tableau pour la liste des comptes
            // nextAccount sera utilisee pour identifier la position ou il faudra rajouter le prochain compte dans le tableau
            var accounts = InitializeAccounts();

            Console.WriteLine("Bienvenue a CCA BANK");
            // exit sera utilisee pour savoir si l'utilisateur a demande de sortir de l'applicaton
            bool exit = false;

            // tant que l'utilisateur n'a pas demande de sortir de l'pplication => la boucle va continuer a tourner
            while (!exit)
            {
                // obtenir le numero de compte de l'utilisateur
                // si Getaccount renvoie -1 => sortir
                var currentAccount = GetAccount(accounts);
                if (currentAccount == null)
                {
                    break;
                }

                // apres avoir trouve le numero de compte valide, obtenir le code pin de l'utilisateur pour ce compte
                // si Authenticate renvoie 'false' => sortir
                if (!Authenticate(currentAccount))
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
                    exit = RunOperations(currentAccount, exit);
                }
            }

            Console.WriteLine("Merci d'avoir utilise CCA BANK");
        }

        /// <summary>
        /// Initialiser une liste de comptes, et renvoyer le nombre de compte dans la liste
        /// </summary>
        /// <param name="tabAccounts">Liste de comptes vide</param>
        /// <returns>Le nombre de compte initialise dans la liste</returns>
        static Dictionary<int, Account> InitializeAccounts()
        {
            var accounts = new Dictionary<int, Account>();
            var acc0 = new SavingAccount(0.1);
            acc0.Number = 100;
            acc0.Pin = 1234;
            acc0.ClientName = "George Cloony";
            
            accounts.Add(acc0.Number, acc0);
            accounts.Add(200, new ChequingAccount() { Number = 200, Pin = 2345, ClientName = "Jon Machin" });
            accounts.Add(300, new SavingAccount(0.09) { Number = 300, Pin = 3456, ClientName = "Forest Moi" });
            return accounts;
        }

        /// <summary>
        /// Obtenir le numero de compte de l'utilisateur
        /// </summary>
        /// <param name="tabAccounts"></param>
        /// <param name="nextAccount"></param>
        /// <returns></returns>
        static Account GetAccount(Dictionary<int, Account> accounts)
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
                    return null;
                }
                // convertir le numero de compte de string a int
                int currentAccountNumber = Convert.ToInt32(tempAccountNumber);

                if (accounts.ContainsKey(currentAccountNumber))
                {
                    return accounts[currentAccountNumber];
                }

                Console.WriteLine("Compte introuvable!");
                continue;
            }
        }

        /// <summary>
        /// Obtenir le code pin de l'utilisateur et verifier que c'est le meme code attache au compte
        /// </summary>
        /// <param name="tabAccounts"></param>
        /// <param name="currentAccount"></param>
        /// <returns></returns>
        static bool Authenticate(Account currentAccount)
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
                if (currentAccount.Pin != currentPin)
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
            Console.WriteLine("3- Payment de facture");
            Console.WriteLine("4- Solde de compte");
            Console.Write("Veuillez saisir le numero de l'operation a effectuer: ");
        }

        /// <summary>
        /// Executer l'operation selectionner par l'utilisateur
        /// </summary>
        /// <param name="tabAccounts"></param>
        /// <param name="currentAccount"></param>
        /// <param name="exit"></param>
        /// <returns></returns>
        static bool RunOperations(Account currentAccount, bool exit)
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
                    exit = Retrait(currentAccount, exit);
                    break;

                // depot
                case "2":
                    exit = Depot(currentAccount, exit);
                    break;

                // payment
                case "3":
                    exit = Paiment(currentAccount, exit);
                    break;

                // solde
                case "4":
                    Console.WriteLine("Votres solde : " + currentAccount.GetBalance());
                    break;

                // autre
                default:
                    Console.WriteLine("Operation invalide");
                    break;
            }
            return exit;
        }

        static bool Paiment(Account currentAccount, bool exit)
        {
            // tant que le retrait n'est pas reussi et l'utilisateur n'a pas demande de sortir, la boucle doit continuer a tourner
            while (true)
            {

                // obtenir le montant a retrer
                Console.Write("Selectinnez la partie a payer : ");
                Console.Write("1- Hydro-Quebec");
                Console.Write("2- Bell Canada");
                Console.Write("3- Gouvernement Du Quebec");
                string tempParty = Console.ReadLine();
                // l'utilisateur peut demander a tout momemnt de sortir de l'application en saisissant 0 ou exit
                if (tempParty == "0" || tempParty == "exit")
                {
                    exit = true;
                    break;
                }

                // convertr le montant de string a int
                int party = Convert.ToInt32(tempParty);

                // obtenir le montant a retrer
                Console.Write("Montant du paiment : ");
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
                var paymentSystem = PaymentSystemFactory.Get(party);
                if (!currentAccount.Paiment(amount, party, paymentSystem))
                {
                    Console.WriteLine("Le montant a retirer doit etre superieur a 0 et inferiuer a votre solde : " + currentAccount.GetBalance());
                    continue;
                }

                Console.WriteLine("Retrait complete");
                Console.WriteLine("Nouveau solde : " + currentAccount.GetBalance());
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
        static bool Retrait(Account currentAccount, bool exit)
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
                if(!currentAccount.Retrait(amount))
                {
                    Console.WriteLine("Le montant a retirer doit etre superieur a 0 et inferiuer a votre solde : " + currentAccount.GetBalance());
                    continue;
                }
                
                Console.WriteLine("Retrait complete");
                Console.WriteLine("Nouveau solde : " + currentAccount.GetBalance());
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
        static bool Depot(Account currentAccount, bool exit)
        {
            // tant que le depot n'est pas reussi et l'utilisateur n'a pas demande de sortir, la boucle doit continuer a tourner
            while (true)
            {
                // obtenir le montant a deposer
                Console.Write("Montant a deposer : ");
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
                if (!currentAccount.Depot(amount))
                {
                    Console.WriteLine("Le montant a deposer doit etre superieur a 0");
                    continue;
                }
                
                Console.WriteLine("Depot complete");
                Console.WriteLine("Nouveau solde : " + currentAccount.GetBalance());
                break;
            }
            return exit;
        }

        static void Collections()
        {
            //
            var list = new List<int>() { 1, 2, 3, 4, 5 };
            var array = new int[] { 1, 2, 3, 4 };
            list.Add(8);

            list.AddRange(new int[] { 10, 11, 12 });

            var has2 = list.Contains(2);
            
            // 
            var accounts = new Dictionary<int, Account>()
            {
                { 100, new ChequingAccount() },
                { 300, new SavingAccount(0.13) }
            };

            accounts.Add(200, new SavingAccount(0.1) { Number = 200 });
            var currentAccount = accounts[200];

            var dic = new Dictionary<string, string>() 
            { 
                { "Key", "Le mot cle a utiliser pour la recherche dans un dictionnaire" },
                { "Value", "La valeur sauvegarde a l'emplacement designe ar la cle dans le dictionnaire" }
            };

            // recherche dans list
            var accountsList = new List<Account>() {
                new SavingAccount(0.1){ Number = 100, ClientName = "John Smith" },
                new ChequingAccount() { Number = 200, ClientName = "Sarah Doe"}
            };


            accountsList.Add(new ChequingAccount() { Number = 300, ClientName = "Bob Ford" });
            var acc = new SavingAccount(0.12) { Number = 400, ClientName = "Tom Robbins" };
            accountsList.Add(acc);

            var s1 = accountsList.FindAll(a => a.ClientName.Contains("ob"));
            foreach (var account in s1)
            {
                Console.WriteLine(account.ClientName);
            }

            accountsList.ForEach(a =>
            {
                a.Number = a.Number * 2;
                Console.WriteLine(a.Number);
            });
                     

            Console.WriteLine(accountsList.Exists(a => HasClient(a, "John Smith")));

            Func<int, int, int> f = (int a, int b) => { return a + b; };
            Console.WriteLine(f(3, 7));
        }

        private static bool HasClient(Account account, string name)
        {
            return account.ClientName == name;
        }

        static void Files()
        {
            var text = "Salut tout le monde";
            try
            {
                File.AppendAllText("Z:/ccabank.txt", text);
            }
            catch(DirectoryNotFoundException dnfe)
            {
                Console.WriteLine("Chemin introuvable : " + dnfe.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }
        }
    }
}
