using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.CardPackage
{
    public class Physical:Card
    {
        public Physical() { }
        public override void SetValid()
        {
            if (!string.IsNullOrEmpty(GetPIN()))
            {
                this.valid = true;
            }
            else
            {
                this.valid = false;
            }
        }
        public bool ReadChip()
        {
            // simulate reading card from ATM
            return true;
        }
    }
}
