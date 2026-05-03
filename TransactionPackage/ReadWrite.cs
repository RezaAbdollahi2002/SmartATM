using FinalProject.CardPackage;
using FinalProject.TransactionPackage;
using System;
using System.Collections.Generic;

namespace FinalProject.TransactionPackages
{
    public class ReadAndWrite
    {
        private List<Transaction> transactions;
        private List<Account> accounts;

        private readonly object transactionLock = new object();
        private readonly object accountLock = new object();

        public ReadAndWrite()
        {
            transactions = new List<Transaction>();
            accounts = new List<Account>();
        }

        public void SaveTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                Console.WriteLine("Transaction is null.");
                return;
            }

            lock (transactionLock)
            {
                transactions.Add(transaction);
            }

            Console.WriteLine("Transaction saved.");
        }

        public Transaction ReadTransaction(string transactionId)
        {
            lock (transactionLock)
            {
                foreach (Transaction transaction in transactions)
                {
                    if (transaction.GetTransactionId() == transactionId)
                    {
                        return transaction;
                    }
                }
            }

            Console.WriteLine("Transaction not found.");
            return null;
        }

        public void SaveAccount(Account account)
        {
            if (account == null)
            {
                Console.WriteLine("Account is null.");
                return;
            }

            lock (accountLock)
            {
                accounts.Add(account);
            }

            Console.WriteLine("Account saved.");
        }

        public Account ReadAccount(string accountId)
        {
            lock (accountLock)
            {
                foreach (Account account in accounts)
                {
                    if (account.GetAccountId() == accountId)
                    {
                        return account;
                    }
                }
            }

            Console.WriteLine("Account not found.");
            return null;
        }

        public void UpdateAccount(Account account)
        {
            if (account == null)
            {
                Console.WriteLine("Account is null.");
                return;
            }

            lock (accountLock)
            {
                for (int i = 0; i < accounts.Count; i++)
                {
                    if (accounts[i].GetAccountId() == account.GetAccountId())
                    {
                        accounts[i] = account;
                        Console.WriteLine("Account updated.");
                        return;
                    }
                }

                accounts.Add(account);
                Console.WriteLine("Account not found. New account saved.");
            }
        }
    }
}