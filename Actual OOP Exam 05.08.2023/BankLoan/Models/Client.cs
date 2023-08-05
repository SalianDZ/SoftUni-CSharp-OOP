using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;

namespace BankLoan.Models
{
    public abstract class Client : IClient
    {
        private string name;
        private string id;
        private double income;

        protected Client(string name, string id, int interest, double income)
        {
            Name = name;
            Id = id;
            Interest = interest;
            Income = income;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.ClientNameNullOrWhitespace));
                }
                name = value;
            }
        }

        public string Id
        {
            get => id;
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.ClientIdNullOrWhitespace));
                }
                id = value;
            }
        }

        public int Interest { get; protected set; }

        public double Income
        {
            get => income;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.ClientIncomeBelowZero));
                }
                income = value;
            }
        }

        public abstract void IncreaseInterest();
    }
}
