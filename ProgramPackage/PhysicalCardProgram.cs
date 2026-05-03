using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.ProgramPackage
{
    internal class PhysicalCardProgram : Program
    {
        public PhysicalCardProgram() { }
        public override void Perform()
        {
            this.context.ReadChip();
            card.SetPIN(context.GetPin());
            card.SetCV2(context.GetCv2());
            card.SetExpirationDate(context.GetExpirationDate());
            card.SetCardNumber(context.GetCardNumber());
        }
    }
}
