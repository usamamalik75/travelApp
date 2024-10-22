using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;

namespace TravelApp
{
    public class TravelAppService(TravelSearchService travelService)
    {
        private readonly TravelSearchService _travelService = travelService;

        public async Task HandleFlightSearch(TravelSearchService travelService)
        {
            Console.WriteLine("\nEnter departure airport code:");
            string departure = Console.ReadLine();

            Console.WriteLine("Enter arrival airport code:");
            string arrival = Console.ReadLine();

            Console.WriteLine("Enter Departure Date (yyyy-MM-dd):");
            string departureDateTime = Console.ReadLine();

            if (departure != null && arrival != null && departureDateTime != null)
            {
                var flights = await travelService.SearchFlightsAsync(departure, arrival, departureDateTime);

                if (flights != null)
                {
                    var displayResults = new DisplayResults();
                    displayResults.Print(flights, "Flights", new[] { "Airline", "Flight Number", "Departure", "Arrival", "Price" });
                }
                else
                {
                    Console.WriteLine("\nNo Flights Found.");
                }
            }
            else
            {
                Console.WriteLine("\nPlease add required infomation.");
            }
        }

        public async Task HandleHotelSearch(TravelSearchService travelService)
        {
            Console.WriteLine("\nEnter the city name:");
            string city = Console.ReadLine();

            if(city != null)
            {
                var hotels = await travelService.SearchHotelsAsync(city);
                if (hotels != null)
                {
                    var displayResults = new DisplayResults();
                    displayResults.Print(hotels, "Hotels", new[] { "Name", "Address", "Rating", "Price Per Night" });
                }
                else
                {
                    Console.WriteLine("\nNo Hotels Found.");
                }
            }
            else
            {
                Console.WriteLine("\nPlease add required infomation.");
            }

        }
    }
}
