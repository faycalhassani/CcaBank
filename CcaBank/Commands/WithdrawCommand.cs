using CcaBank.Business;
using CcaBank.Security;
using System;

namespace CcaBank.Commands
{
    class WithdrawCommand : BaseCommand
    {
        public WithdrawCommand()
        {
            Id = "1";
            Description = "Retrait";
        }

        public override bool Run(Session session)
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
                    return false;
                }

                // convertr le montant de string a int
                int amount = Convert.ToInt32(tempAmount);

                // effectuer le retrait avec l'objet Account
                if (!session.Account.Retrait(amount))
                {
                    Console.WriteLine("Le montant a retirer doit etre superieur a 0 et inferiuer a votre solde : " + session.Account.GetBalance());
                    continue;
                }

                Console.WriteLine("Retrait complete");
                Console.WriteLine("Nouveau solde : " + session.Account.GetBalance());
                return true;
            }
        }

    }
}
