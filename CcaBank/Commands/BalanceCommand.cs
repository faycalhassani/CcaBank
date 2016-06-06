using CcaBank.Security;
using System;

namespace CcaBank.Commands
{
    class BalanceCommand: BaseCommand
    {
        public BalanceCommand()
        {
            Id = "4";
            Description = "Solde de compte";
        }

        public override bool Run(Session session)
        {
            Console.WriteLine("Votres solde : " + session.Account.GetBalance());
            return true;
        }
    }
}
