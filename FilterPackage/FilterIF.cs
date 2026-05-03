using FinalProject.ATMPackage;
using FinalProject.CompanyPackage;
using FinalProject.TransactionPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.FilterPackage
{
    public interface FilterIF:ObservableIF
    {
        void SetTransaction(TransactionIF transaction);
        TransactionIF GetTransaction();
        void SetFilter(FilterIF filter);
        FilterIF GetFilter();
        void Register(CompanyIF company);
        void RemoveObserver();
        void SetData(Data data);
        Data GetData();
    }

}
