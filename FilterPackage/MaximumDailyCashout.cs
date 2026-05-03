using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.FilterPackage
{
    public class MaximumDailyCashout: Filter
    {
        public MaximumDailyCashout() { }
        public override bool Check()
        {
            if (this.data.GetMaximumCashOut() < this.transaction.GetAmount()) { 
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
