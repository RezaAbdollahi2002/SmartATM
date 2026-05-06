using FinalProject.ATMPackage;
using FinalProject.CardPackage;
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
        private Data data;

        public Transaction(CardIF card)
        {
            this.card = card;
            this.transactionId = Guid.NewGuid().ToString();
        }

        public void SetTransactionId(string transactionId) { this.transactionId = transactionId; }
        public string GetTransactionId() { return transactionId; }

        public void SetCard(CardIF card) { this.card = card; }
        public CardIF GetCard() { return card; }

        public void SetAmount(decimal amount) { this.amount = amount; }
        public decimal GetAmount() { return amount; }

        public void SetCard2(CardIF card) { this.card2 = card; }
        public CardIF GetCard2() { return card2; }

        public void SetStatus(bool status) { this.status = status; }
        public bool GetStatus() { return status; }

        public void SetData(Data data) { this.data = data; }
        public Data GetData() { return data; }

        public void Deposit()
        {
            if (card == null || !card.GetValid() || amount <= 0 || data == null || card.GetAccount() == null)
            {
                status = false;
                return;
            }

            Account account = card.GetAccount();

            account.Deposit(amount);
            data.DepositCash(amount);

            status = true;
        }

        public void Withdrawal()
        {
            if (card == null || !card.GetValid() || amount <= 0 || data == null || card.GetAccount() == null)
            {
                status = false;
                return;
            }

            Account account = card.GetAccount();

            if (!data.TryWithdrawCash(amount))
            {
                status = false;
                return;
            }

            if (!account.Withdraw(amount))
            {
                status = false;
                return;
            }

            status = true;
        }

        public void Transfer()
        {
            if (card == null || card2 == null || !card.GetValid() || !card2.GetValid() || amount <= 0)
            {
                status = false;
                return;
            }

            Account from = card.GetAccount();
            Account to = card2.GetAccount();

            if (from == null || to == null || from == to)
            {
                status = false;
                return;
            }

            if (!from.Withdraw(amount))
            {
                status = false;
                return;
            }

            to.Deposit(amount);

            status = true;
        }
    }
}