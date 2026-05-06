using FinalProject.ATMPackage;
using FinalProject.CompanyPackage;
using FinalProject.TransactionPackage;

namespace FinalProject.FilterPackage
{
    public abstract class Filter : FilterIF
    {
        protected TransactionIF transaction;
        protected FilterIF filter;
        protected Data data;

        public Filter(FilterIF filter) {
            this.filter = filter;
        }

        public void SetTransaction(TransactionIF transaction) { this.transaction = transaction; }
        public TransactionIF GetTransaction() { return transaction; }

        public void SetFilter(FilterIF filter) { this.filter = filter; }
        public FilterIF GetFilter() { return filter; }

        public void SetData(Data data) { this.data = data; }
        public Data GetData() { return data; }
        public abstract bool Check(TransactionIF transaction);
    }
}