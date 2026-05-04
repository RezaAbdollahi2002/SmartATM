namespace FinalProject.FilterPackage
{
    public class DefaultFilter : Filter
    {
        public DefaultFilter() { }

        public override bool Check()
        {
            if (this.transaction == null)
            {
                return false;
            }

            if (this.transaction.GetCard() == null)
            {
                return false;
            }

            if (this.transaction.GetAmount() <= 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.transaction.GetTransactionId()))
            {
                return false;
            }

            return true;
        }
    }
}