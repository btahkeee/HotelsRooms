using HotelsRooms.Models;
using Microsoft.Extensions.DependencyInjection;
using HotelsRooms.Interfaces;
using HotelsRooms.Services;
namespace HotelsRooms
{
    class Program
    {

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Build the service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Use the service
            var jsonService = serviceProvider.GetService<IJsonHandler>();
            var hotelService = serviceProvider.GetService<IHotelService>();
            var hotels = jsonService.LoadJsonFile<List<Hotel>>("D://hotels.json");
            var bookings = jsonService.LoadJsonFile<List<Booking>>("D://bookings.json");  

            string input;
            //while (!string.IsNullOrEmpty(input = Console.ReadLine()))
            //{
            //    Console.WriteLine($"availability for :{input}");
            //    var parts = input.Split(' ', ',', StringSplitOptions.RemoveEmptyEntries);
            //    if (parts.Length < 3)
            //    {
            //        Console.WriteLine("Invalid input format. Use: Availability(H1, 20240901, SGL)");
            //        continue;
            //    }

            //    string hotelId = parts[0].Replace("Availability(", "").Trim(',');
            //    string dateOrRange = parts[1].Trim(',');
            //    string roomType = parts[2].TrimEnd(')');

           // //Parse date range
           //var dateRange = dateOrRange.Contains('-') ?
           //    hotelService.CreateDateRange(dateOrRange.Split('-')[0], dateOrRange.Split('-')[1]) :
           //    new List<string> { dateOrRange };

           // int availableRooms = hotelService.CheckAvailability(hotels, bookings, hotelId, roomType, dateRange);
           // Console.WriteLine($"Available {roomType} rooms: {availableRooms}");
        //}

        var list = new List<string>();
            list.Add("Availability(H1, 20240905-20240908, DBL)");
            list.Add("Availability(H1, 20240901-20240902, DBL)");
            list.Add("Availability(H1, 20240901-20240903, DBL)");
            list.Add("Availability(H1, 20240902-20240903, DBL)");
            list.Add("Availability(H1, 20240903-20240904, DBL)");
            list.Add("Availability(H1, 20240904-20240905, DBL)");

            foreach (var item in list)
            {
                Console.WriteLine($"availability for :{item}");
                var parts = item.Split(' ', ',', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3)
                {
                    Console.WriteLine("Invalid input format. Use: Availability(H1, 20240901, SGL)");
                    continue;
                }

                string hotelId = parts[0].Replace("Availability(", "").Trim(',');
                string dateOrRange = parts[1].Trim(',');
                string roomType = parts[2].TrimEnd(')');

                // Parse date range
                var dateRange = dateOrRange.Contains('-') ?
                    hotelService.CreateDateRange(dateOrRange.Split('-')[0], dateOrRange.Split('-')[1]) :
                    new List<string> { dateOrRange };

                int availableRooms = hotelService.GetAvailableRooms(hotels, bookings, hotelId, roomType, dateRange);
                Console.WriteLine($"Available {roomType} rooms: {availableRooms}");
            }
            
            
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IJsonHandler, JsonHandler>();
            services.AddScoped<IHotelService, HotelService>();
        }
    }
}
