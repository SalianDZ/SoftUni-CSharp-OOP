namespace BankLoan.Models
{
    public class Student : Client
    {
        private const int DefaultInterestRate = 2;
        public Student(string name, string id, double income)
            : base(name, id, DefaultInterestRate, income)
        {
        }

        public override void IncreaseInterest()
        {
            Interest += 1;
        }
    }
}
