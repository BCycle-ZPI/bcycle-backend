using System.ComponentModel.DataAnnotations;

namespace bcycle_backend.Models.Entities
{
    public class TripPhoto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string PhotoUrl { get; set; }
        public int TripId { get; set; }

        public Trip Trip { get; set; }
    }
}
