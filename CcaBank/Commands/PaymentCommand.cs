using CcaBank.Business;
using CcaBank.Payment;
using CcaBank.Security;
using System;

namespace CcaBank.Commands
{
    class PaymentCommand : BaseCommand
    {
        public PaymentCommand()
        {
            Id = "3";
            Description = "Paiment de facture";
        }

        public override bool Run(Session session)
        {
            // tant que le retrait n'est pas reussi et l'utilisateur n'a pas demande de sortir, la boucle doit continuer a tourner
            while (true)
            {

                // obtenir le montant a retrer
                Console.WriteLine("Selectinnez la partie a payer : ");
                Console.WriteLine("1- Hydro-Quebec");
                Console.WriteLine("2- Bell Canada");
                Console.WriteLine("3- Gouvernement Du Quebec");
                string tempParty = Console.ReadLine();
                // l'utilisateur peut demander a tout momemnt de sortir de l'application en saisissant 0 ou exit
                if (tempParty == "0" || tempParty == "exit")
                {
                    return false;
                }

                // convertr le montant de string a int
                int party = Convert.ToInt32(tempParty);

                // obtenir le montant a retrer
                Console.Write("Montant du paiment : ");
                string tempAmount = Console.ReadLine();
                // l'utilisateur peut demander a tout momemnt de sortir de l'application en saisissant 0 ou exit
                if (tempAmount == "0" || tempAmount == "exit")
                {
                    return false;
                }

                // convertr le montant de string a int
                int amount = Convert.ToInt32(tempAmount);

                // effectuer le retrait avec l'objet Account
                var paymentSystem = new PaymentSystemFactory().Get(party);
                if (!session.Account.Paiment(amount, party, paymentSystem))
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
