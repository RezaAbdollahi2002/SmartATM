namespace FinalProject.FilterPackage
{
    public class CheckAvailableCash : Filter
    {
        public CheckAvailableCash() { }

        public override bool Check()
        {
            if (data == null || transaction == null) return false;

            if (transaction.GetAmount() > data.GetCash())
            {
                Notify();
                return false;
            }

            return CheckNext();
        }

        private void Notify()
        {
            if (company != null)
            {
                company.Notify();
            }
        }
    }
}