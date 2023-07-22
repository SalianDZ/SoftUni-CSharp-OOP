namespace ChristmasPastryShop.Models.Delicacies
{
    public class Gingerbread : Delicacy
    {
        public const double GignerBreadPrice = 4.00;
        public Gingerbread(string name)
            : base(name, GignerBreadPrice)
        {
        }
    }
}
