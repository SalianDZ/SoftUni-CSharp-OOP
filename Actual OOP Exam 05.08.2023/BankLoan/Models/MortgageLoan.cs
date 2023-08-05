namespace BankLoan.Models
{
    public class MortgageLoan : Loan
    {
        private const int DefaultInterestRate = 3;
        private const double DefaultAmount = 50000;
        public MortgageLoan()
            : base(DefaultInterestRate, DefaultAmount)
        {
        }
    }
}
