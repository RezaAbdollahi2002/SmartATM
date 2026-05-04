namespace FinalProject.FilterPackage
{
    public class CheckAvailableCash : Filter
    {
        public CheckAvailableCash() { }

        public override bool Check()
        {
            if (this.data == null || this.transaction == null)
            {
                return false;
            }

            if (this.transaction.GetAmount() > this.data.GetCash())
            {
                Notify();
                return false;
            }

            return true;
        }

        private void Notify()
        {
            if (this.company != null)
            {
                this.company.Notify();
            }
        }
    }
}