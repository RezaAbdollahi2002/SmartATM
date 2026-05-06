namespace FinalProject.FilterPackage
{
    public class DefaultFilter : Filter
    {
        public DefaultFilter() { }

        public override bool Check()
        {
            if (transaction == null) return false;
            if (transaction.GetCard() == null) return false;
            if (!transaction.GetCard().GetValid()) return false;
            if (transaction.GetCard().GetAccount() == null) return false;
            if (transaction.GetAmount() <= 0) return false;
            if (string.IsNullOrWhiteSpace(transaction.GetTransactionId())) return false;

            return CheckNext();
        }
    }
}