using Newtonsoft.Json.Linq;
using TravelApp.Models;

namespace TravelApp
{
    public class TravelSearchService(ApiClient apiClient)
    {
        private readonly ApiClient _apiClient = apiClient;

        public Task<Flight[]> SearchFlightsAsync(string departure, string arrival, string departureDateTime)
        {
            string endpoint = $"flights?origin={departure}&destination={arrival}&departuredate ={departureDateTime}";

            Console.WriteLine("\nSearching for flights...");

            return SearchAsync(endpoint, ParseFlight);
        }

        public Task<Hotel[]> SearchHotelsAsync(string city)
        {
            string endpoint = $"hotels?city={city}";

            Console.WriteLine("\nSearching for hotels...");

            return SearchAsync(endpoint, ParseHotel);
        }

        private async Task<T[]> SearchAsync<T>(string apiUrl, Func<JToken, T> parser)
        {
            string response = await _apiClient.GetAsync(apiUrl);

            if(response is not null)
            {
                return ParseApiResponse(response, parser);
            }

            return [];
        }

        // Generic parsing method
        private static T[] ParseApiResponse<T>(string jsonData, Func<JToken, T> parser)
        {
            JObject json = JObject.Parse(jsonData);
            var items = new List<T>();

            foreach (var item in json["results"])
            {
                items.Add(parser(item));
            }

            return items.ToArray();
        }

        private Flight ParseFlight(JToken item)
        {
            return new Flight
            {
                Airline = item["airline"].ToString(),
                FlightNumber = item["flight_number"].ToString(),
                DepartureTime = item["departure_time"].ToString(),
                ArrivalTime = item["arrival_time"].ToString(),
                Price = decimal.Parse(item["price"].ToString())
            };
        }

        private Hotel ParseHotel(JToken item)
        {
            return new Hotel
            {
                Name = item["name"].ToString(),
                Address = item["address"].ToString(),
                Rating = item["rating"].ToString(),
                PricePerNight = decimal.Parse(item["price_per_night"].ToString())
            };
        }
    }
}
