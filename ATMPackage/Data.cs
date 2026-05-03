namespace FinalProject.ATMPackage
{
    public class Data
    {
        private decimal availableCash;
        private decimal maximumCashOut;
        private readonly object dataLock = new object();

        public Data() { }

        public void SetCash(decimal cash)
        {
            lock (dataLock)
            {
                this.availableCash = cash;
            }
        }

        public decimal GetCash()
        {
            lock (dataLock)
            {
                return this.availableCash;
            }
        }

        public void SetMaximumCashOut(decimal cash)
        {
            lock (dataLock)
            {
                this.maximumCashOut = cash;
            }
        }

        public decimal GetMaximumCashOut()
        {
            lock (dataLock)
            {
                return this.maximumCashOut;
            }
        }

        public bool TryWithdrawCash(decimal amount)
        {
            lock (dataLock)
            {
                if (amount <= 0)
                    return false;

                if (amount > availableCash)
                    return false;

                if (amount > maximumCashOut)
                    return false;

                availableCash -= amount;
                return true;
            }
        }

        public void DepositCash(decimal amount)
        {
            lock (dataLock)
            {
                if (amount > 0)
                {
                    availableCash += amount;
                }
            }
        }
    }
}