using FinalProject.ATMPackage;
using FinalProject.CompanyPackage;
using FinalProject.TransactionPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.FilterPackage
{
    public abstract class Filter:FilterIF
    {
        protected TransactionIF transaction;
        protected FilterIF filter;
        protected CompanyIF company;
        protected Data data;

        public Filter() { }
        public void SetTransaction(TransactionIF transaction) { transaction = transaction; }
        public TransactionIF GetTransaction() { return transaction; }
        public void SetFilter(FilterIF filter) { this.filter = filter; }
        public FilterIF GetFilter() { return filter; }
        public void Register(CompanyIF company) { this.company = company; }
        public void RemoveObserver() { company = null; }

        public void SetData(Data data) { this.data = data; }
        public Data GetData() {  return this.data; } 
        public abstract bool Check();

    }
}
