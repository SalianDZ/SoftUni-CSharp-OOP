namespace ChristmasPastryShop.Models.Delicacies
{
    public class Stolen : Delicacy
    {
        public const double GignerBreadPrice = 3.50;
        public Stolen(string name)
            : base(name, GignerBreadPrice)
        {

        }
    }
}
