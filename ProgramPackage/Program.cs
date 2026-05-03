using FinalProject.CardPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.ProgramPackage
{
    public abstract class Program: ProgramIF
    {
        protected CardIF card;
        protected Context context;
        public Program() { }
        public void SetCard(CardIF card)
        {
            this.card = card;
        }
        public CardIF GetCard() { return this.card; }

        public void SetContext(Context context)
        {
            this.context = context;
        }
        public Context GetContext() { return this.context; }

        public abstract void Perform();
    }
}
