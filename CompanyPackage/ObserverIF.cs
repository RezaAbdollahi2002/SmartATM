using FinalProject.ATMPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.CompanyPackage
{
    public interface ObserverIF
    {
        void AddATM(ObservableIF atm);
        List<ObservableIF> GetATMs();
        void Notify();

    }
}
