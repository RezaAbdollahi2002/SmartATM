using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.FilterPackage
{
    public class CheckAvailableCash: Filter
    {
        public CheckAvailableCash() { }
        public override bool Check()
        {
            if (this.data.GetCash() <= this.transaction.GetAmount())
            {
                Notify();
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Notify()
        {
            this.company.Notify();
        }
    }
}
