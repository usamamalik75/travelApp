namespace TravelApp.Models
{
    public class Hotel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rating { get; set; }
        public decimal PricePerNight { get; set; }

        public override string ToString()
        {
            return $"| {Name,-25} | {Address,-30} | {Rating,-5} | {PricePerNight,10:C} |";
        }
    }
}
