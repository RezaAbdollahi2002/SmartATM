using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.ProgramPackage
{
    public class Context
    {
        private string pin;
        private string accountNumber;
        private string cv2;
        private string expirationDate;
       
        public Context() { }



        // PIN
        public void SetPin(string pin)
        {
            this.pin = pin;
        }

        public string GetPin()
        {
            return this.pin;
        }

        // CHIP
        public void ReadChip()
        {
            this.accountNumber = "1234-1235-1259-4896";
            this.cv2 = "156";
            this.expirationDate = "05/24";
            this.pin = "1459";
    }

        // ACCOUNT NUMBER
        public void SetCardNumber(string accountNumber)
        {
            this.accountNumber = accountNumber;
        }

        public string GetCardNumber()
        {
            return this.accountNumber;
        }

        // CV2
        public void SetCv2(string cv2)
        {
            this.cv2 = cv2;
        }

        public string GetCv2()
        {
            return this.cv2;
        }

        // ExpirationDate
        public void SetExpirationDate(string exp)
        {
            this.expirationDate = exp;
        }

        public string GetExpirationDate()
        {
            return this.expirationDate;
        }
    }
}
