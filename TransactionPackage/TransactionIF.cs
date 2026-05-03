using FinalProject.ATMPackage;
using FinalProject.CardPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.TransactionPackage
{
    public interface TransactionIF
    {
        void SetCard(CardIF card);
        CardIF GetCard();

        void SetAmount(decimal ammount);
        decimal GetAmount();

        void SetCard2(CardIF card);
        CardIF GetCard2();
        void SetTransactionId(string transactionId);
        string GetTransactionId();
        void SetStatus(bool status);
        bool GetStatus();
        void SetData(Data data);
        Data GetData();
        void Deposit();
        void Withdrawal();
        void Transfer();
    }
}
