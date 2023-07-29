namespace PlanetWars.Models.Weapons
{
    public class NuclearWeapon : Weapon
    {
        private const double DefaultPrice = 15;
        public NuclearWeapon(int destructionLevel)
            : base(destructionLevel, DefaultPrice)
        {
        }
    }
}
