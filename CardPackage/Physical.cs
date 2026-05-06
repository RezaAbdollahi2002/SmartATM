namespace FinalProject.CardPackage
{
    public class Physical : Card
    {
        public Physical() { }

        public override void SetValid()
        {
            valid = ReadChip() &&
                    !string.IsNullOrWhiteSpace(GetPIN()) &&
                    !string.IsNullOrWhiteSpace(GetCardNumber()) &&
                    !string.IsNullOrWhiteSpace(GetCV2()) &&
                    !string.IsNullOrWhiteSpace(GetExpirationDate());
        }

        public bool ReadChip()
        {
            return true;
        }
    }
}