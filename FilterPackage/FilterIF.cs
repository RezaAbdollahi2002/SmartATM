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
    public interface FilterIF
    {
       bool Check(TransactionIF transaction);
    }

}
