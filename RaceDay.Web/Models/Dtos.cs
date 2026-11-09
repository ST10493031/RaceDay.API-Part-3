namespace RaceDay.Web.Models
{
    public class LoginDto { public string Email { get; set; } = ""; public string Password { get; set; } = ""; }
    public class RegisterDto { public string FullName { get; set; } = ""; public string LastName { get; set; } = ""; public string Email { get; set; } = ""; public string Password { get; set; } = ""; public string Role { get; set; } = ""; public string Phone { get; set; } = ""; }
    public class LoginResponseDto { public string Token { get; set; } = ""; public string Role { get; set; } = ""; public int UserId { get; set; } }
    public class CreateResultDto { public TimeSpan FinishTime { get; set; } public int OverallPosition { get; set; } public int CategoryPosition { get; set; } public string Pace { get; set; } = ""; }

    public class EventDto
    {
        public int EventID { get; set; }
        public string EventName { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = "";
        public string EventType { get; set; } = "";
        public string? BannerUrl { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();
    }

    public class CategoryDto { public int CategoryID { get; set; } public int EventID { get; set; } public string CategoryName { get; set; } = ""; public string Gender { get; set; } = ""; public int MinAge { get; set; } public int MaxAge { get; set; } public double Distance { get; set; } public decimal EntryFee { get; set; } }
    public class CreateCategoryDto { public string CategoryName { get; set; } = ""; public string Gender { get; set; } = "mixed"; public int MinAge { get; set; } public int MaxAge { get; set; } public double Distance { get; set; } public decimal EntryFee { get; set; } }

    public class EnrolmentDto { public int EnrolmentID { get; set; } public string EventName { get; set; } = ""; public string CategoryName { get; set; } = ""; public string Status { get; set; } = ""; public string PaymentStatus { get; set; } = ""; }
}