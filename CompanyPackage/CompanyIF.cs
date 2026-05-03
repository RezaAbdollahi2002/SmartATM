using FinalProject.FilterPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.CompanyPackage
{
    public interface CompanyIF 
    {
        void AddATM(ObservableIF atm);
        List<ObservableIF> GetATM();
        void Notify();
    }
}
