using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.CardPackage
{
    public class CardFactory
    {
        private CardIF card;
        public CardFactory() { }

        public void SetCard(string name)
        {
            Type type = typeof(Card);
            card = (CardIF)Activator.CreateInstance(type);
        }
        public CardIF GetCard() {return this.card;}
    }
}
