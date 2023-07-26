namespace BookingApp.Models.Rooms
{
    public class Apartment : Room
    {
        private const int DefaultBedCapacity = 6;
        public Apartment()
            : base(DefaultBedCapacity)
        {
        }
    }
}
