namespace BookingApp.Models.Rooms
{
    public class Studio : Room
    {
        private const int DefaultBedCapacity = 4;
        public Studio()
            : base(DefaultBedCapacity)
        {
        }
    }
}
