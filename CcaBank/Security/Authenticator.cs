using CcaBank.Business;
using System;
using System.Collections.Generic;

namespace CcaBank.Security
{
    class Authenticator
    {
        private Dictionary<int, Account> _accounts;

        public Authenticator(Dictionary<int, Account> accounts)
        {
            _accounts = accounts;
        }

        public Session Authenticate()
        {
            // obtenir le numero de compte de l'utilisateur
            // si Getaccount renvoie -1 => sortir
            var currentAccount = GetAccount(_accounts);
            if (currentAccount == null)
            {
                return null;
            }

            // apres avoir trouve le numero de compte valide, obtenir le code pin de l'utilisateur pour ce compte
            // si Authenticate renvoie 'false' => sortir
            if (!ValidatePin(currentAccount))
            {
                return null;
            }

            var session = new Session() { Account = currentAccount };
            return session;
        }


        /// <summary>
        /// Obtenir le numero de compte de l'utilisateur
        /// </summary>
        /// <param name="tabAccounts"></param>
        /// <param name="nextAccount"></param>
        /// <returns></returns>
        private Account GetAccount(Dictionary<int, Account> accounts)
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
        private bool ValidatePin(Account currentAccount)
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

    }

}
