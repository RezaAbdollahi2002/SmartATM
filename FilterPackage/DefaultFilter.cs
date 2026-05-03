using FinalProject.TransactionPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.FilterPackage
{
    public class DefaultFilter: Filter
    {
        public DefaultFilter() { }  
        public override bool Check()
        {
            if (this.transaction.GetCard() == null || this.transaction.GetAmount() <= 0 || this.transaction.GetTransactionId() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
