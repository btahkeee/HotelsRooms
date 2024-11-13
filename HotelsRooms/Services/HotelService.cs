using HotelsRooms.Interfaces;
using HotelsRooms.Models;

namespace HotelsRooms.Services
{
    public class HotelService : IHotelService
    {
        public List<string> CreateDateRange(string startDate, string endDate)
        {
            DateTime start = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime end = DateTime.ParseExact(endDate, "yyyyMMdd", null);
            var dateRange = new List<string>();

            for (var date = start; date <= end; date = date.AddDays(1))
            {
                dateRange.Add(date.ToString("yyyyMMdd"));
            }

            return dateRange;
        }

        public int GetAvailableRooms(List<Hotel> hotels, List<Booking> bookings, string hotelId, string roomType, List<string> dateRange)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == hotelId);
            if (hotel == null)
            {
                Console.WriteLine($"Hotel {hotelId} not found.");
                return 0;
            }

            int totalRooms = hotel.Rooms.Count(r => r.RoomType == roomType);
            if (totalRooms == 0)
            {
                Console.WriteLine($"No rooms of type {roomType} available.");
                return 0;
            }

            int bookedRooms = 0;
            foreach (var date in dateRange)
            {
                bookedRooms += bookings.Count(b => b.HotelId == hotelId && b.RoomType == roomType &&
                                                   DateTime.ParseExact(b.Arrival, "yyyyMMdd", null) <= DateTime.ParseExact(date, "yyyyMMdd", null) &&
                                                   DateTime.ParseExact(b.Departure, "yyyyMMdd", null) >= DateTime.ParseExact(date, "yyyyMMdd", null));
            }

            return totalRooms - bookedRooms;
        }
    }
}
