using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookigApp.Tests
{
    public class Tests
    {
        [Test]
        public void RoomConstructorTest()
        {
            Room room = new Room(4, 60);
            Assert.IsNotNull(room);
            Assert.AreEqual(room.BedCapacity, 4);
            Assert.AreEqual(room.PricePerNight, 60);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        public void RoomBedCapacityNegativeCase(int value)
        {
            Assert.Throws<ArgumentException>(() => new Room(value, 60));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        public void RoomPricePerNightNegativeCase(double value)
        {
            Assert.Throws<ArgumentException>(() => new Room(4, value));
        }

        [Test]
        public void BookingConstructorTest()
        {
            Room room = new Room(4, 60);
            Booking booking = new Booking(10, room, 3);
            Assert.IsNotNull(booking);
            Assert.AreEqual(booking.Room.BedCapacity, 4);
            Assert.AreEqual(booking.Room.PricePerNight, 60);
            Assert.AreEqual(booking.BookingNumber, 10);
            Assert.AreEqual(booking.ResidenceDuration, 3);
            Assert.AreEqual(booking.Room, room);
        }

        [Test]
        public void BookingConstructorTestIfRoomIsNull()
        {
            Booking booking = new Booking(10, null, 3);
            Assert.IsNotNull(booking);
            Assert.IsNull(booking.Room);
        }

        [Test]
        public void HotelConstructorTest()
        {
            Room room = new Room(4, 60);
            Booking booking = new Booking(10, room, 3);
            Hotel hotel = new Hotel("Velingrad", 5);
            Assert.IsNotNull(hotel);
            Assert.AreEqual(hotel.FullName, "Velingrad");
            Assert.AreEqual(hotel.Category, 5);
            Assert.AreEqual(hotel.Rooms, new List<Room> ());
            Assert.AreEqual(hotel.Bookings, new List<Booking>());
            Assert.AreEqual(hotel.Rooms.Count,0);
            Assert.AreEqual(hotel.Bookings.Count, 0);

            Type type = typeof(Hotel);
            FieldInfo fieldInfo = type.GetField("turnover", BindingFlags.NonPublic | BindingFlags.Instance);
            double turnover = (double)fieldInfo.GetValue(hotel);
            Assert.AreEqual(turnover, 0);
        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void HotelNameNegativeCase(string value)
        {
            Assert.Throws<ArgumentNullException>(() => new Hotel(value, 6));
        }


        [Test]
        [TestCase(0)]
        [TestCase(6)]
        public void HotelCategoryNegativeCase(int value)
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Velingrad", value));
        }

        [Test]
        public void HotelAddRoomMethodTest()
        {
            Room room1 = new Room(4, 60);
            Room room2 = new Room(2, 640);
            Hotel hotel = new Hotel("Velingrad", 5);
            int expectedRoomsCountInHotelBeforeAdding = 0;
            int actualRoomsCountInHotelBeforeAdding = hotel.Rooms.Count;
            hotel.AddRoom(room1);
            hotel.AddRoom(room2);
            int expectedRoomsCountInHotelAfterAdding = 2;
            int actualRoomsCountInHotelAfterAdding = hotel.Rooms.Count;
            Assert.AreEqual(expectedRoomsCountInHotelBeforeAdding, actualRoomsCountInHotelBeforeAdding);
            Assert.AreEqual(expectedRoomsCountInHotelAfterAdding, actualRoomsCountInHotelAfterAdding);
        }

        [Test]
        public void TestIfHotelBookRoomMethodActuallyBooksARoom()
        {
            Room room1 = new Room(4, 60);
            Room room2 = new Room(2, 640);
            Hotel hotel = new Hotel("Velingrad", 5);
            hotel.AddRoom(room1);
            hotel.AddRoom(room2);
            hotel.BookRoom(2, 2, 3, 200);
            Assert.AreEqual(hotel.Bookings.Count , 1);
            Assert.AreEqual(hotel.Bookings.FirstOrDefault().Room, room1);
            Assert.AreEqual(hotel.Bookings.FirstOrDefault().ResidenceDuration, 3);
            Type type = typeof(Hotel);
            FieldInfo fieldInfo = type.GetField("turnover", BindingFlags.NonPublic | BindingFlags.Instance);
            double turnover = (double)fieldInfo.GetValue(hotel);
            Assert.AreEqual(turnover, 180);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        public void TestIfHotelBookRoomMethodThrowsExceptionIfAdultsAreZeroOrNegativeNumber(int value)
        {
            Hotel hotel = new Hotel("Velingrad", 5);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(value, 2, 3, 200));
        }

        [Test]
        public void TestIfHotelBookRoomMethodThrowsExceptionIfChildrenAreNegativeNumber()
        {
            Hotel hotel = new Hotel("Velingrad", 5);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, -2, 3, 200));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-5)]
        public void TestIfHotelBookRoomMethodThrowsExceptionIfResidenceDurationIsZeroOrNegativeNumber(int value)
        {
            Hotel hotel = new Hotel("Velingrad", 5);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, 2, value, 200));
        }

        [Test]
        public void TestIfHotelBookRoomMethodDoesNotBookARoomWhenThereIsNoSuitableRoom()
        { 
            Room room1 = new Room(2, 60);
            Hotel hotel = new Hotel("Velingrad", 5);
            hotel.AddRoom(room1);
            hotel.BookRoom(2, 2, 3, 200);
            Assert.AreEqual(hotel.Bookings.Count, 0);
        }

        [Test]
        public void TestIfHotelBookRoomMethodDoesNotBookARoomWhenBudgetIsNotEnough()
        {
            Room room1 = new Room(2, 1000);
            Hotel hotel = new Hotel("Velingrad", 5);
            hotel.AddRoom(room1);
            hotel.BookRoom(2, 2, 3, 200);
            Assert.AreEqual(hotel.Bookings.Count, 0);
        }

        [Test]
        public void TestIfHotelBookRoomMethodDoesNotBookARoomWhenThereAreNoRooms()
        {
            Hotel hotel = new Hotel("Velingrad", 5);
            hotel.BookRoom(2, 2, 3, 200);
            Assert.AreEqual(hotel.Bookings.Count, 0);
        }
    }
}