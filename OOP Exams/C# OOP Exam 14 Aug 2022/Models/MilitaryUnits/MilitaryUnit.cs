using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Utilities.Messages;
using System;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {
        protected MilitaryUnit(double cost)
        {
            Cost = cost;
            EnduranceLevel = 1;
        }

        public double Cost { get; private set; }

        public int EnduranceLevel { get; private set; }

        public void IncreaseEndurance()
        {
            EnduranceLevel++;

            if (EnduranceLevel > 20)
            {
                EnduranceLevel = 20;
                string result = String.Format(ExceptionMessages.EnduranceLevelExceeded);
                throw new ArgumentException(result);
            }
        }
    }
}
