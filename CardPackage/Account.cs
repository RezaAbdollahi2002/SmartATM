using System;

namespace FinalProject.CardPackage
{
    public class Account
    {
        private string accountId;
        private decimal balance;
        private readonly object balanceLock = new object();

        public Account() { }

        public void SetAccountId(string accountId)
        {
            this.accountId = accountId;
        }

        public string GetAccountId()
        {
            return this.accountId;
        }

        public void SetBalance(decimal balance)
        {
            lock (balanceLock)
            {
                this.balance = balance;
            }
        }

        public decimal GetBalance()
        {
            lock (balanceLock)
            {
                return this.balance;
            }
        }
    }
}