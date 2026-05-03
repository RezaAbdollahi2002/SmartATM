using FinalProject.CardPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.ProgramPackage
{
    public interface ProgramIF
    {
        void SetCard(CardIF card);
        CardIF GetCard();
        void Perform();
        void SetContext(Context context);
        Context GetContext();

    }
}
