using FinalProject.ATMPackage;
using FinalProject.CardPackage;
using FinalProject.TransactionPackages;
using System;

namespace FinalProject.TransactionPackage
{
    public class Transaction : TransactionIF
    {
        private string transactionId;
        private CardIF card;
        private decimal amount;
        private CardIF card2;
        private bool status;
        private ReadAndWrite readWrite;
        private Data data;
        public Transaction(CardIF card)
        {
            this.card = card;
            this.transactionId = Guid.NewGuid().ToString();
        }

        public void SetTransactionId(string transactionId)
        {
            this.transactionId = transactionId;
        }

        public string GetTransactionId()
        {
            return transactionId;
        }

        public void SetCard(CardIF card)
        {
            this.card = card;
        }

        public CardIF GetCard()
        {
            return card;
        }

        public void SetAmount(decimal amount)
        {
            this.amount = amount;
        }

        public decimal GetAmount()
        {
            return amount;
        }

        public void SetCard2(CardIF card)
        {
            this.card2 = card;
        }

        public CardIF GetCard2()
        {
            return card2;
        }

        public void SetStatus(bool status)
        {
            this.status = status;
        }

        public bool GetStatus()
        {
            return status;
        }

        public void SetReadAndWrite(ReadAndWrite readWrite)
        {
            this.readWrite = readWrite;
        }

        public ReadAndWrite GetReadAndWrite()
        {
            return readWrite;
        }

        public void SetData(Data data) { this.data = data; }
        public Data GetData() { return data; }

        public void Deposit()
        {
            if (card == null || !card.GetValid())
            {
                Console.WriteLine("Invalid card.");
                status = false;
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount.");
                status = false;
                return;
            }

            if (data == null)
            {
                Console.WriteLine("ATM data not set.");
                status = false;
                return;
            }

            Account account = card.GetAccount();

            lock (account)
            {
                account.SetBalance(account.GetBalance() + amount);
            }

            // 🔵 ATM cash increases
            data.DepositCash(amount);

            status = true;
            Console.WriteLine("Deposit successful.");
        }

        public void Withdrawal()
        {
            if (card == null || !card.GetValid())
            {
                Console.WriteLine("Invalid card.");
                status = false;
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
                status = false;
                return;
            }

            if (data == null)
            {
                Console.WriteLine("ATM data not set.");
                status = false;
                return;
            }

            Account account = card.GetAccount();

            lock (account)
            {
                if (account.GetBalance() < amount)
                {
                    Console.WriteLine("Insufficient balance.");
                    status = false;
                    return;
                }

                if (!data.TryWithdrawCash(amount))
                {
                    Console.WriteLine("ATM does not have enough cash or exceeds max limit.");
                    status = false;
                    return;
                }

                account.SetBalance(account.GetBalance() - amount);
            }

            status = true;
            Console.WriteLine("Withdrawal successful.");
        }

        public void Transfer()
        {
            if (card == null || card2 == null)
            {
                Console.WriteLine("Cards not set.");
                status = false;
                return;
            }

            if (!card.GetValid() || !card2.GetValid())
            {
                Console.WriteLine("Invalid card.");
                status = false;
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Invalid transfer amount.");
                status = false;
                return;
            }

            Account from = card.GetAccount();
            Account to = card2.GetAccount();

            object firstLock = from.GetHashCode() < to.GetHashCode() ? from : to;
            object secondLock = from.GetHashCode() < to.GetHashCode() ? to : from;

            lock (firstLock)
            {
                lock (secondLock)
                {
                    if (from.GetBalance() < amount)
                    {
                        Console.WriteLine("Insufficient balance.");
                        status = false;
                        return;
                    }

                    from.SetBalance(from.GetBalance() - amount);
                    to.SetBalance(to.GetBalance() + amount);
                }
            }

            status = true;
            Console.WriteLine("Transfer successful.");
        }
    }
}
