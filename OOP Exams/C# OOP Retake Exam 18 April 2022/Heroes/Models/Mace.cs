namespace Heroes.Models
{
    public class Mace : Weapon
    {
        private const int DefaultDamage = 25;
        public Mace(string name, int durability)
            : base(name, durability)
        {
        }

        public override int DoDamage()
        {
            if (Durability > 0)
            {
                this.Durability--;

                return 25;
            }

            return 0;
        }
    }
}
