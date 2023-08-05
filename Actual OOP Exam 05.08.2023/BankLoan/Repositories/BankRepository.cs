using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        private List<IBank> banks = new();
        public IReadOnlyCollection<IBank> Models => banks.AsReadOnly();

        public void AddModel(IBank model)
        {
            banks.Add(model);
        }

        public IBank FirstModel(string name)
        {
            return banks.FirstOrDefault(x => x.Name == name);
        }

        public bool RemoveModel(IBank model)
        {
            return banks.Remove(model);
        }
    }
}
