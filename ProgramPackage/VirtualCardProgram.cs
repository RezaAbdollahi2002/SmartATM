using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.ProgramPackage
{
    public class VirtualCardProgram:Program
    {
        public VirtualCardProgram() { }
        public override void Perform()
        {
            card.SetPIN(context.GetPin());
            card.SetCV2(context.GetCv2());
            card.SetExpirationDate(context.GetExpirationDate());
            card.SetCardNumber(context.GetCardNumber());

        }
    }
}
