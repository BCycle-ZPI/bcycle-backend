namespace bcycle_backend.Models.Entities
{
    public class TripPhoto
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public int TripId { get; set; }

        public Trip Trip { get; set; }
    }
}
