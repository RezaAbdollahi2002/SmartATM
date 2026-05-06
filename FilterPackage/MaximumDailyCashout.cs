using FinalProject.TransactionPackage;

namespace FinalProject.FilterPackage
{
    public class MaximumDailyCashout : Filter
    {
        public MaximumDailyCashout(FilterIF filter):base(filter) { }

        public override bool Check(TransactionIF transaction)
        {
            if (data == null || transaction == null) return false;
            if (transaction.GetAmount() > data.GetMaximumCashOut()) return false;

            return true;
        }
    }
}