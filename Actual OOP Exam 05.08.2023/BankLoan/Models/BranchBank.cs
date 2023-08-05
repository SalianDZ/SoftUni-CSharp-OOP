namespace BankLoan.Models
{
    public class BranchBank : Bank
    {
        private const int DefaultCapacity = 25;
        public BranchBank(string name)
            : base(name, DefaultCapacity)
        {
        }
    }
}
