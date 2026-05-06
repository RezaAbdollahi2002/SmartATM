using FinalProject.CardPackage;
using System;
using System.Collections.Generic;

namespace FinalProject.TransactionPackage
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
            if (transaction == null) return;
            lock (transactionLock)
            {
                transactions.Add(transaction);
            }
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
            return null;
        }

        public List<Transaction> GetTransactions()
        {
            lock (transactionLock)
            {
                return new List<Transaction>(transactions);
            }
        }

        public void SaveAccount(Account account)
        {
            if (account == null) return;
            lock (accountLock)
            {
                accounts.Add(account);
            }
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
            return null;
        }

        public void UpdateAccount(Account account)
        {
            if (account == null) return;

            lock (accountLock)
            {
                for (int i = 0; i < accounts.Count; i++)
                {
                    if (accounts[i].GetAccountId() == account.GetAccountId())
                    {
                        accounts[i] = account;
                        return;
                    }
                }

                accounts.Add(account);
            }
        }
    }
}