using FinalProject.TransactionPackage;

namespace FinalProject.FilterPackage
{
    public class CheckAvailableCash : Filter
    {
        public CheckAvailableCash(FilterIF filter):base(filter) { }

        public override bool Check(TransactionIF transaction)
        {
            if (data == null || transaction == null) return false;

            if (transaction.GetAmount() > data.GetCash())
            {
                this.GetData().Notify();
                return false;
            }
            return true;    
        }
    }
}