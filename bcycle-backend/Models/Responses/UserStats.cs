namespace bcycle_backend.Models.Responses
{
    public class UserStats
    {
        public int TripCount { get; set; }
        public int GroupTripTotalCount { get; set; }
        public int GroupTripHostingCount { get; set; }
        public double TotalKilometers { get; set; }
        public double TotalTimeMinutes { get; set; }
    }
}
