using HotelsRooms.Models;

namespace HotelsRooms.Interfaces
{
    public interface IHotelService
    {
        public List<string> CreateDateRange(string startDate, string endDate);
        public int GetAvailableRooms(
            List<Hotel> hotels,
            List<Booking> bookings,
            string hotelId,
            string roomType,
            List<string> dateRange);
    }
}
