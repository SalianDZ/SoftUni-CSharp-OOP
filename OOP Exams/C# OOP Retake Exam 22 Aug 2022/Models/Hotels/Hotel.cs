using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Linq;

namespace BookingApp.Models.Hotels
{
    public class Hotel : IHotel
    {
        private string name;
        private int category;
        private double turnOver;


        public Hotel(string fullName, int category)
        {
            FullName = fullName;
            Category = category;
            Bookings = new BookingRepository();
            Rooms = new RoomRepository();
        }
        public string FullName
        {
            get => name;
            private set
            {
                if (String.IsNullOrEmpty(value))
                {
                    string result = String.Format(ExceptionMessages.HotelNameNullOrEmpty);
                    throw new ArgumentException(result);
                }
                name = value;
            }
        }

        public int Category
        {
            get => category;
            private set
            {
                if (value < 1 || value > 5)
                {
                    string result = String.Format(ExceptionMessages.InvalidCategory);
                    throw new ArgumentException(result);
                }
                category = value;
            }
        }

        public double Turnover => Math.Round(Bookings.All().Sum(x => x.ResidenceDuration * x.Room.PricePerNight), 2);

        public IRepository<IRoom> Rooms { get; set; }

        public IRepository<IBooking> Bookings { get; set; }
    }
}
