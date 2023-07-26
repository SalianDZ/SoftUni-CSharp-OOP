using BookingApp.Models.Rooms.Contracts;
using System;
using BookingApp.Utilities.Messages;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        private double pricePerNight = 0;
        private int bedCapacity;

        protected Room(int bedCapacity)
        {
            this.bedCapacity = bedCapacity;
        }

        public int BedCapacity => bedCapacity;

        public double PricePerNight
        {
            get { return pricePerNight; }
            private set
            {
                if (value < 0)
                {
                    string result = String.Format(ExceptionMessages.PricePerNightNegative);
                    throw new ArgumentException(result);
                }
                pricePerNight = value;
            }
        }

        public void SetPrice(double price)
        {
            PricePerNight = price;
        }
    }
}
