using System;

namespace FinalProject.CardPackage
{
    public class CardFactory
    {
        private CardIF card;

        public CardFactory() { }

        public void SetCard(string name)
        {
            if (name == "Physical")
            {
                card = new Physical();
            }
            else if (name == "Virtual")
            {
                card = new Virtual();
            }
            else
            {
                throw new ArgumentException("Invalid card type.");
            }
        }

        public CardIF GetCard()
        {
            return this.card;
        }
    }
}