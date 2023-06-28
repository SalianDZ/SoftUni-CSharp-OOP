using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding
{
    public class Druid : BaseHero
    {
        private const int HeroPower = 80;
        public Druid(string name) : base(name)
        {
            Power = HeroPower;
        }

        public override string CastAbility()
        {
            return $"{GetType().Name} - {Name} healed for {Power}";
        }
    }
}
