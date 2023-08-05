namespace BankLoan.Models
{
    public class Adult : Client
    {
        private const int DefaultInterestRate = 4;
        public Adult(string name, string id, double income)
            : base(name, id, DefaultInterestRate, income)
        {
        }

        public override void IncreaseInterest()
        {
            Interest += 2;
        }
    }
}
