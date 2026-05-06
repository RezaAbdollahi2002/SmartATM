using FinalProject.ATMPackage;
using FinalProject.FilterPackage;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FinalProject.CompanyPackage
{
    public class Company : CompanyIF
    {
        private List<ObservableIF> atms;
        private Data data;

        public Company()
        {
            atms = new List<ObservableIF>();
        }

        public void AddATM(ObservableIF atm)
        {
            if (atm != null && !atms.Contains(atm))
            {
                atms.Add(atm);
            }
        }

        public List<ObservableIF> GetATM()
        {
            return atms;
        }

        public void SetData(Data data)
        {
            this.data = data;
        }

        public Data GetData()
        {
            return data;
        }

        public void Notify()
        {
            MessageBox.Show("The company was notified because the ATM does not have enough available cash.", "Company Notification");
        }
    }
}