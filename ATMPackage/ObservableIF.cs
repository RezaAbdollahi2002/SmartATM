using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FinalProject.CompanyPackage;
using FinalProject.FilterPackage;

namespace FinalProject.ATMPackage
{
    public interface ObservableIF
    {
        void Register(ObserverIF observer);
        ObserverIF GetObserver();
        void Notify();
        void SetCash(decimal cash);
        decimal GetCash();
        void SetMaximumCashOut(decimal cash);
        decimal GetMaximumCashOut();
        bool TryWithdrawCash(decimal amount);
        void DepositCash(decimal amount);
    }
}
