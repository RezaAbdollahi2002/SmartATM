using FinalProject.ProgramPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.CardPackage
{
    public abstract class Card:CardIF
    {
        private Account account;
        private ProgramIF program;
        private string PIN;
        private string cv2;
        private string cardNumber;
        private string expirationDate;
        protected bool valid;

        public Card() { }
        public void SetAccount(Account account)
        {
            this.account = account;
        }
        public Account GetAccount() {return this.account;}  
        public void SetPIN(string pin) { this.PIN = pin;}
        public string GetPIN() {return this.PIN;}   
        public void SetCV2(string cv2) { this.cv2 = cv2; }
        public string GetCV2() { return this.cv2; }
        public void SetCardNumber(string cardNumber) { this.cardNumber = cardNumber;}
        public string GetCardNumber() { return this.cardNumber;}
        public void SetExpirationDate(string expirationDate) { this.expirationDate = expirationDate;}
        public string GetExpirationDate() {  return this.expirationDate;}

        public abstract void SetValid();
        public bool GetValid() {return this.valid;}
    }
}
