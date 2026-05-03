using FinalProject.FilterPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.CompanyPackage
{
    public class Company : CompanyIF
    {
        private List<ObservableIF> atms;
        public Company() {
            atms = new List<ObservableIF>();
        }
        public void AddATM(ObservableIF atm)
        {
        this.atms.Add(atm);
        }
        public List<ObservableIF> GetATM()
        {
            return this.atms;
        }
        public void Notify() {
            MessageBox.Show("The company was notified.");
        }
    }
}
