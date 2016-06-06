using CcaBank.Business;
using CcaBank.Security;
using System;

namespace CcaBank.Commands
{
    class DepositCommand : BaseCommand
    {
        public DepositCommand()
        {
            Id = "2";
            Description = "Depot";
        }

        public override bool Run(Session session)
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
                    return false;
                }

                // convertir le montant a deposer de string a int
                int amount = Convert.ToInt32(tempAmount);
                // effectuer le depot avec l'objet Account
                if (!session.Account.Depot(amount))
                {
                    Console.WriteLine("Le montant a deposer doit etre superieur a 0");
                    continue;
                }

                Console.WriteLine("Depot complete");
                Console.WriteLine("Nouveau solde : " + session.Account.GetBalance());
                return true;
            }
        }
    }
}
