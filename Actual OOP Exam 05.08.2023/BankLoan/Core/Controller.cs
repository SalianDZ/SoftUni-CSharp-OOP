using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private LoanRepository loans;
        private BankRepository banks;

        public Controller()
        {
            loans = new();
            banks = new();
        }
        public string AddBank(string bankTypeName, string name)
        {
            IBank bank;
            if (bankTypeName == "BranchBank")
            {
                bank = new BranchBank(name);
            }
            else if (bankTypeName == "CentralBank")
            {
                bank = new CentralBank(name);
            }
            else
            {
                throw new ArgumentException(String.Format(ExceptionMessages.BankTypeInvalid));
            }

            banks.AddModel(bank);
            return String.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        } // done

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            IBank bank = banks.FirstModel(bankName);
            IClient client;
            if (clientTypeName == "Student")
            {
                //BranchBank
                if (bank.GetType().Name != "BranchBank")
                {
                    throw new ArgumentException(String.Format(OutputMessages.UnsuitableBank));
                }
                client = new Student(clientName, id, income);
            }
            else if (clientTypeName == "Adult")
            {
                //CentralBank
                if (bank.GetType().Name != "CentralBank")
                {
                    throw new ArgumentException(String.Format(OutputMessages.UnsuitableBank));
                }
                client = new Adult(clientName, id, income);
            }
            else
            {
                throw new ArgumentException(String.Format(ExceptionMessages.ClientTypeInvalid));
            }

            bank.AddClient(client);
            return String.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
        } // done

        public string AddLoan(string loanTypeName)
        {
            ILoan loan;
            if (loanTypeName == "StudentLoan")
            {
                loan = new StudentLoan();
            }
            else if (loanTypeName == "MortgageLoan")
            {
                loan = new MortgageLoan();
            }
            else
            {
                throw new ArgumentException(String.Format(ExceptionMessages.LoanTypeInvalid));
            }
            loans.AddModel(loan);
            return String.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
        } // done

        public string FinalCalculation(string bankName)
        {
            IBank bank = banks.FirstModel(bankName);
            double totalIncome = bank.Clients.Sum(x => x.Income) + bank.Loans.Sum(x => x.Amount);
            return $"The funds of bank {bankName} are {totalIncome:f2}.";
        } // done

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            IBank bank = banks.FirstModel(bankName);
            ILoan loan = loans.FirstModel(loanTypeName);

            if (loan == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.MissingLoanFromType, loanTypeName));
            }

            bank.AddLoan(loan);
            loans.RemoveModel(loan);
            return String.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
        } // done

        public string Statistics()
        {
            StringBuilder sb = new();
            foreach (var bank in banks.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
