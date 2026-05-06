using System;
using FinalProject.TransactionPackages;

namespace FinalProject.CardPackage
{
    public class Account
    {
        private string accountId;
        private decimal balance;

        // Read/Write lock manager
        private ReadWrite readWrite;

        public Account()
        {
            readWrite = new ReadWrite();
        }

        public void SetReadWrite(ReadWrite readWrite)
        {
            this.readWrite = readWrite;
        }

        public ReadWrite GetReadWrite()
        {
            return readWrite;
        }

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
            readWrite.WriteLock();

            try
            {
                this.balance = balance;
            }
            finally
            {
                readWrite.Done();
            }
        }

        public decimal GetBalance()
        {
            readWrite.ReadLock();

            try
            {
                return this.balance;
            }
            finally
            {
                readWrite.Done();
            }
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount.");
                return;
            }

            readWrite.WriteLock();

            try
            {
                balance += amount;
            }
            finally
            {
                readWrite.Done();
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
                return false;
            }

            readWrite.WriteLock();

            try
            {
                if (balance < amount)
                {
                    Console.WriteLine("Insufficient balance.");
                    return false;
                }

                balance -= amount;
                return true;
            }
            finally
            {
                readWrite.Done();
            }
        }
    }
}