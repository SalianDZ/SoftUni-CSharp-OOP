using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding
{
    public class Warrior : BaseHero
    {
        public const int HeroPower = 100;
        public Warrior(string name) : base(name)
        {
            Power = HeroPower;
        }

        public override string CastAbility()
        {
            return $"{GetType().Name} - {Name} hit for {Power} damage";

        }
    }
}
