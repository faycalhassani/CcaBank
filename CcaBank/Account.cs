using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcaBank
{
    class Account
    {
        public int Number { get; set; }
        public int Pin { get; set; }
        public int Balance { get; private set; }
        public string ClientName { get; set; }

        public bool Depot(int amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            Balance += amount;
            return true;
        }

        public bool Retrait(int amount)
        {
            if (amount <= 0 || amount > Balance)
            {
                return false;
            }
            Balance -= amount;
            return true;
        }
    }
}
