using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.CardPackage
{
    public interface CardIF
    {
        void SetAccount(Account account);
        Account GetAccount();

        void SetPIN(string pin);
        string GetPIN();

        void SetCV2(string cv2);
        string GetCV2();

        void SetCardNumber(string number);
        string GetCardNumber();

        void SetExpirationDate(string expirationDate);
        string GetExpirationDate();

        void SetValid();
        bool GetValid();
    }
}
