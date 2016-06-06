using CcaBank.Security;
using System;
using System.Collections.Generic;

namespace CcaBank.Commands
{
    class CommandManager
    {
        public List<ICommand> Commands { get; private set; }
        private Session _session;

        public CommandManager(Session session)
        {
            Commands = new List<ICommand>();
            _session = session;

            // enregistrer les commands
            Add(new DepositCommand());
            Add(new WithdrawCommand());
            Add(new PaymentCommand());
            Add(new BalanceCommand());
        }

        public void Add(ICommand command)
        {
            Commands.Add(command);
        }

        public bool Run(string commandId)
        {
            var command = Commands.Find(c => c.GetId() == commandId);
            if (command == null)
            {
                Console.WriteLine("Operation inconnue");
                return true;
            }
            return command.Run(_session);
        }
    }
}
