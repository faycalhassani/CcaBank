﻿using System;
using System.Collections.Generic;

namespace CcaBank.Business
{
    /// <summary>
    /// Represente un compte bancaire
    /// </summary>
    public abstract class Account
    {
        public static string BankCode { get; set; }
        public int Number { get; set; }
        public int Pin { get; set; }        
        public string ClientName { get; set; }
        protected int Balance;

        /// <summary>
        /// Liste des operations pour ce compte
        /// </summary>
        public List<Operation> Operations { get; private set; }
        protected static int NextOperation = 1;

        /// <summary>
        /// Constructeur de la class Account. 
        /// Il sert a initialiser des valeurs au moment de la createion d'un comte
        /// </summary>
        public Account()
        {
            Operations = new List<Operation>();
        }

        public Account(int initialBalance) : this()
        {
            Balance = initialBalance;
        }

        /// <summary>
        /// Obtenir le solde du compte
        /// virtual = le comportement de cette fonction peut etre ecrase dans une sous-classe
        /// </summary>
        /// <returns></returns>
        public abstract int GetBalance();

        /// <summary>
        /// Operation pour effectuer un depot dans le compte
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool Depot(int amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            Balance += amount;
            // ajouter le retrait a la lists des operations
            LogOperation(amount);
            return true;
        }

        public bool Depot(int amount, DateTime date)
        {
            Depot(amount);
            // faire que;que chose avec la date
            return true;
        }

        /// <summary>
        /// Operation pour effectuer un retrait du compte
        /// </summary>
        /// <param name="amount">Montant a retirer. Doit etre superieur a zero (0) et inferieur au solde du compte</param>
        /// <returns></returns>
        public bool Retrait(int amount)
        {
            if (amount <= 0 || amount > Balance)
            {
                return false;
            }
            Balance -= amount;
            // ajouter le retrait a la lists des operations
            LogOperation(-amount);
            return true;
        }

        /// <summary>
        /// Ajouter une operation a la liste des peration et renvoyer le nombre d'operation dans la liste
        /// </summary>
        /// <param name="tabOperations"></param>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <param name="nextOperation"></param>
        private void LogOperation(int amount)
        {
            Operations.Add(new Operation { AccountNumber = Number, Number = NextOperation, Amount = amount, Date = DateTime.Now });
            NextOperation++;
        }

        public bool Paiment(int amount, int party, IPaymentSystem paymentSystem)
        {
            if (paymentSystem.Pay(amount))
            {
                return true;
            }
            return false;
        }
    }
}
