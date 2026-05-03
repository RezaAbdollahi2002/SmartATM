using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.CompanyPackage;

namespace FinalProject.FilterPackage
{
    public interface ObservableIF
    {
        void Register(CompanyIF company);
        void RemoveObserver();
        bool Check();
    }
}
