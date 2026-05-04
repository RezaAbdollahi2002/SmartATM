namespace FinalProject.FilterPackage
{
    public class MaximumDailyCashout : Filter
    {
        public MaximumDailyCashout() { }

        public override bool Check()
        {
            if (this.data == null || this.transaction == null)
            {
                return false;
            }

            if (this.transaction.GetAmount() > this.data.GetMaximumCashOut())
            {
                return false;
            }

            return true;
        }
    }
}