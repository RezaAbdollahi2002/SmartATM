using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.CardPackage
{
    public class Virtual:Card
    {
        public Virtual() { }

        public override void SetValid()
        {
            if (!string.IsNullOrEmpty(GetPIN()) &&
                !string.IsNullOrEmpty(GetCardNumber()) &&
                !string.IsNullOrEmpty(GetCV2()) &&
                !string.IsNullOrEmpty(GetExpirationDate()))
            {
                this.valid = true;
            }
            else
            {
                this.valid = false;
            }
        }

        public bool VerifyOnline()
        {
            // simulate API call to bank
            return true;
        }
    }
}
