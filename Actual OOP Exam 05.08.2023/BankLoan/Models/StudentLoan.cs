namespace BankLoan.Models
{
    public class StudentLoan : Loan
    {
        private const int DefaultInterestRate = 1;
        private const double DefaultAmount = 10000;
        public StudentLoan()
            : base(DefaultInterestRate, DefaultAmount)
        {
        }
    }
}
