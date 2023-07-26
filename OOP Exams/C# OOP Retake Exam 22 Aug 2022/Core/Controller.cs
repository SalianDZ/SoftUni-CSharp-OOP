using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private HotelRepository hotels = new();
        public string AddHotel(string hotelName, int category)
        {
            string result;
            if (hotels.All().Any(x => x.FullName == hotelName))
            {
                result = String.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
                throw new ArgumentException(result);
            }

            Hotel hotel = new(hotelName, category);
            hotels.AddNew(hotel);
            result = String.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
            return result;
        } //done

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            List<IHotel> wantedHotels = hotels.All().Where(x => x.Category == category).OrderBy(x => x.FullName).ToList();
            string result;
            if (wantedHotels.Count == 0)
            {
                result = String.Format(OutputMessages.CategoryInvalid, category);
                throw new ArgumentException(result);
            }

            List<IRoom> wantedRooms = new();

            foreach (var hotel in wantedHotels)
            {
                foreach (var currentRoom in hotel.Rooms.All().Where(x => x.PricePerNight > 0 && x.BedCapacity >= children + adults))
                {
                    wantedRooms.Add(currentRoom);
                }
            }

            IRoom room = wantedRooms.OrderBy(x => x.BedCapacity).FirstOrDefault();

            if (room == null)
            {
                result = String.Format(OutputMessages.RoomNotAppropriate);
                throw new ArgumentException(result);
            }

            IHotel wantedHotel = hotels.All().Where(x => x.Rooms.All().Contains(room)).FirstOrDefault();
            Booking booking = new Booking(room, duration, adults, children, wantedHotel.Bookings.All().Count + 1);
            wantedHotel.Bookings.AddNew(booking);
            result = String.Format(OutputMessages.BookingSuccessful, wantedHotel.Bookings.All().Count, wantedHotel.FullName);
            return result;
        } // done

        public string HotelReport(string hotelName)
        {
            string result;
            IHotel hotel = hotels.All().FirstOrDefault(x => x.FullName == hotelName);
            if (hotel == null)
            {
                result = String.Format(OutputMessages.HotelNameInvalid, hotelName);
                throw new ArgumentException(result);
            }

            StringBuilder sb = new();
            sb.AppendLine($"Hotel name: {hotelName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine($"--Bookings:");
            sb.AppendLine();
            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
            }
            else
            {
                foreach (var booking in hotel.Bookings.All())
                {
                    sb.AppendLine(booking.BookingSummary());
                    sb.AppendLine();
                }
            }
            return sb.ToString().TrimEnd();
        } 

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            string result;
            if (!hotels.All().Any(x => x.FullName == hotelName))
            {
                result = String.Format(OutputMessages.HotelNameInvalid, hotelName);
                throw new ArgumentException(result);
            }

            IHotel hotel = hotels.All().FirstOrDefault(x => x.FullName == hotelName);

            if (roomTypeName != "DoubleBed" && roomTypeName != "Studio" && roomTypeName != "Apartment")
            {
                result = String.Format(ExceptionMessages.RoomTypeIncorrect);
                throw new ArgumentException(result);
            }

            if (!hotel.Rooms.All().Any(x => x.GetType().Name == roomTypeName))
            {
                result = String.Format(OutputMessages.RoomTypeNotCreated);
                throw new ArgumentException(result);
            }

            IRoom room = hotel.Rooms.All().FirstOrDefault(x => x.GetType().Name == roomTypeName);

            if (room.PricePerNight != 0)
            {
                result = String.Format(ExceptionMessages.PriceAlreadySet);
                throw new InvalidOperationException(result);
            }

            room.SetPrice(price);
            result = String.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
            return result;
        } // done

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            string result;
            if (!hotels.All().Any(x => x.FullName == hotelName))
            {
                result = String.Format(OutputMessages.HotelNameInvalid, hotelName);
                throw new ArgumentException(result);
            }

            IHotel hotel = hotels.All().FirstOrDefault(x => x.FullName == hotelName);

            if (hotel.Rooms.All().Any(x => x.GetType().Name == roomTypeName))
            {
                result = String.Format(OutputMessages.RoomTypeAlreadyCreated);
                throw new ArgumentException(result);
            }

            IRoom room;
            if (roomTypeName == "DoubleBed")
            {
                room = new DoubleBed();
            }
            else if (roomTypeName == "Studio")
            {
                room = new Studio();
            }
            else if (roomTypeName == "Apartment")
            {
                room = new Apartment();
            }
            else
            {
                result = String.Format(ExceptionMessages.RoomTypeIncorrect);
                throw new ArgumentException(result);
            }

            result = String.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
            hotel.Rooms.AddNew(room);
            return result;
        } // done
    }
}
