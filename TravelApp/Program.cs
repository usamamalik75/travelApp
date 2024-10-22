
namespace TravelApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the Travel App!");

            // Initialize the SabreAuthService
            var authService = new SabreAuthService();

            // Initialize ApiClient with SabreAuthService
            var apiClient = new ApiClient(authService);

            // Initialize Flight and Hotel services
            var travelService = new TravelSearchService(apiClient);

            var travelAppService = new TravelAppService(travelService);

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Search Flights");
                Console.WriteLine("2. Search Hotels");
                Console.WriteLine("3. Exit");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await travelAppService.HandleFlightSearch(travelService);
                        break;
                    case "2":
                        await travelAppService.HandleHotelSearch(travelService);
                        break;
                    case "3":
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
