using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Rooms.Contracts;
using System;
using BookingApp.Utilities.Messages;
using System.Text;

namespace BookingApp.Models.Bookings
{
    public class Booking : IBooking
    {
        private int residenceDuration;
        private int adultsCount;
        private int childrenCount;
        private int bookingNumber;

        public Booking(IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber)
        {
            Room = room;
            ResidenceDuration = residenceDuration;
            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            this.bookingNumber = bookingNumber;
        }

        public IRoom Room { get; private set; }

        public int ResidenceDuration
        {
            get => residenceDuration;
            private set
            {
                if (value <= 0)
                {
                    string result = String.Format(ExceptionMessages.DurationZeroOrLess);
                    throw new ArgumentException(result);
                }
                residenceDuration = value;
            }
        }

        public int AdultsCount
        {
            get => adultsCount;
            private set
            {
                if (value <= 0)
                {
                    string result = String.Format(ExceptionMessages.AdultsZeroOrLess);
                    throw new ArgumentException(result);
                }
                adultsCount = value;
            }
        }

        public int ChildrenCount
        {
            get => childrenCount;
            private set
            {
                if (value < 0)
                {
                    string result = String.Format(ExceptionMessages.ChildrenNegative);
                    throw new ArgumentException(result);
                }
                childrenCount = value;
            }
        }

        public int BookingNumber => bookingNumber;


        private double TotalPaid() => Math.Round(ResidenceDuration * Room.PricePerNight, 2);

        public string BookingSummary()
        {
            StringBuilder result = new();
            result.AppendLine($"Booking number: {BookingNumber}");
            result.AppendLine($"Room type: {Room.GetType().Name}");
            result.AppendLine($"Adults: {AdultsCount} Children: {ChildrenCount}");
            result.AppendLine($"Total amount paid: {TotalPaid():F2} $");
            return result.ToString().TrimEnd();
        }
    }
}
