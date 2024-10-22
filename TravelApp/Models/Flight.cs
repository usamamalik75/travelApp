namespace TravelApp.Models
{
    public class Flight
    {
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"| {Airline,-15} | {FlightNumber,-10} | {DepartureTime,-15} | {ArrivalTime,-15} | {Price,10:C} |";
        }
    }
}
