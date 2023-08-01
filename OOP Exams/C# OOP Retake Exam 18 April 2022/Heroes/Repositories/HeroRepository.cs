using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private List<IHero> heroes = new List<IHero>();
        public IReadOnlyCollection<IHero> Models => heroes.AsReadOnly();

        public void Add(IHero model)
        {
            heroes.Add(model);
        }

        public IHero FindByName(string name)
        {
            return heroes.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IHero model)
        {
            return heroes.Remove(model);
        }
    }
}
