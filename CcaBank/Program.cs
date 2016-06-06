using CcaBank.Business;
using CcaBank.Commands;
using CcaBank.Data;
using CcaBank.Security;
using System;
using System.Linq;

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
            // message de bienvenue
            Console.WriteLine("Bienvenue a CCA BANK");

            // initialiser le code de la banque
            Account.BankCode = "003";
            // initialiser les structure de donnees et les donnees
            var data = new DataContext();
            
            // initialiser l'authentificateur
            var authenticator = new Authenticator(data.Accounts);
            // authentifier l'utilisateur
            var session = authenticator.Authenticate();
            
            if (session != null)
            {
                // initialiser le command manager
                var commandManager = new CommandManager(session);

                // l'utilisateur doit etre capable d'effectuer plusieurs operation
                // tant que l'utilisateur n'a pas demande de sortir => la boucle va continuer a tourner
                // l'application doit lui afficher le menu et lui proposer de selectionner une operation a effectuer
                while (true)
                {
                    // afficher menu
                    Menu(commandManager);

                    // si entree == 0 sortir
                    var op = Console.ReadLine();
                    if (op == "0")
                    {
                        break;
                    }

                    // effectuer l'operation selectionnee par l'utilisateur
                    if (!commandManager.Run(op))
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("Merci d'avoir utilise CCA BANK. A Bientot!");
        }

        /// <summary>
        /// Afficher le menu
        /// La fonction parcours les commandes enregistrees dans le command manager et affiche leur texte
        /// </summary>
        static void Menu(CommandManager commandManager)
        {
            Console.WriteLine("Operations disponible :");
            Console.WriteLine("0 - Sortie");
            foreach (var command in commandManager.Commands.OrderBy(c => c.GetId()))
            {
                Console.WriteLine(command.DisplayText());
            }            
            Console.Write("Veuillez saisir le numero de l'operation a effectuer: ");
        }

    }
}
