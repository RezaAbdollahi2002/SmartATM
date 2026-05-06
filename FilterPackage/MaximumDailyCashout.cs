namespace FinalProject.FilterPackage
{
    public class MaximumDailyCashout : Filter
    {
        public MaximumDailyCashout() { }

        public override bool Check()
        {
            if (data == null || transaction == null) return false;
            if (transaction.GetAmount() > data.GetMaximumCashOut()) return false;

            return CheckNext();
        }
    }
}