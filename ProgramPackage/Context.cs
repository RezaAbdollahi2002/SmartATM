namespace FinalProject.ProgramPackage
{
    public class Context
    {
        private string pin;
        private string accountNumber;
        private string cv2;
        private string expirationDate;

        public Context() { }

        public void SetPin(string pin) { this.pin = pin; }
        public string GetPin() { return this.pin; }

        // Simulates the ATM reading the physical chip data.
        // It does NOT overwrite the user-entered PIN.
        public void ReadChip()
        {
            this.accountNumber = "1234-1235-1259-4896";
            this.cv2 = "156";
            this.expirationDate = "05/29";
        }

        public void SetCardNumber(string accountNumber) { this.accountNumber = accountNumber; }
        public string GetCardNumber() { return this.accountNumber; }

        public void SetCv2(string cv2) { this.cv2 = cv2; }
        public string GetCv2() { return this.cv2; }

        public void SetExpirationDate(string exp) { this.expirationDate = exp; }
        public string GetExpirationDate() { return this.expirationDate; }
    }
}