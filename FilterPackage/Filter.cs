using FinalProject.ATMPackage;
using FinalProject.CompanyPackage;
using FinalProject.TransactionPackage;

namespace FinalProject.FilterPackage
{
    public abstract class Filter : FilterIF
    {
        protected TransactionIF transaction;
        protected FilterIF filter;
        protected CompanyIF company;
        protected Data data;

        public Filter() { }

        public void SetTransaction(TransactionIF transaction)
        {
            this.transaction = transaction;
        }

        public TransactionIF GetTransaction()
        {
            return this.transaction;
        }

        public void SetFilter(FilterIF filter)
        {
            this.filter = filter;
        }

        public FilterIF GetFilter()
        {
            return this.filter;
        }

        public void Register(CompanyIF company)
        {
            this.company = company;
        }

        public void RemoveObserver()
        {
            this.company = null;
        }

        public void SetData(Data data)
        {
            this.data = data;
        }

        public Data GetData()
        {
            return this.data;
        }

        public abstract bool Check();
    }
}