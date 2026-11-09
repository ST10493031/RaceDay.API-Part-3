using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RaceDay.API.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public int UserID { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;

        // 👇 Added BannerUrl
        public string? BannerUrl { get; set; }

        // Navigation properties
        [JsonIgnore]
        public User? Organiser { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        // 👇 Added Routes back in (assuming you named the class EventRoute)
        public ICollection<EventRoute> Routes { get; set; } = new List<EventRoute>();
    }
}