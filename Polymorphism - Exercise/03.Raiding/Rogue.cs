using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding
{
    public class Rogue : BaseHero
    {
        private const int HeroPower = 80;
        public Rogue(string name) : base(name)
        {
            Power = HeroPower;
        }

        public override string CastAbility()
        {
            return $"{GetType().Name} - {Name} hit for {Power} damage";
        }
    }
}
