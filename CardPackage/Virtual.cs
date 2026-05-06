namespace FinalProject.CardPackage
{
    public class Virtual : Card
    {
        public Virtual() { }

        public override void SetValid()
        {
            valid = VerifyOnline() &&
                    !string.IsNullOrWhiteSpace(GetPIN()) &&
                    !string.IsNullOrWhiteSpace(GetCardNumber()) &&
                    !string.IsNullOrWhiteSpace(GetCV2()) &&
                    !string.IsNullOrWhiteSpace(GetExpirationDate());
        }

        public bool VerifyOnline()
        {
            return true;
        }
    }
}