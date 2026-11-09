using RaceDay.Web.Models;
namespace RaceDay.Web.Services
{
    public interface IRaceDayApiClient
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto);
        Task<(bool Success, LoginResponseDto? Data)> LoginAsync(LoginDto dto);
        Task<List<EventDto>> GetEventsAsync();
        Task<List<dynamic>> GetEventEnrolmentsAsync(int eventId);
        Task<(bool Success, string Message)> CreateResultAsync(int enrolmentId, CreateResultDto dto);
        Task<List<dynamic>> GetEventResultsAsync(int eventId);
        Task<EventDto?> GetEventAsync(int id);
        Task<(bool Success, int EventId)> CreateEventAsync(EventDto dto);
        Task<(bool Success, string Message)> UploadBannerAsync(int eventId, Stream fileStream, string fileName);
        Task<(bool Success, string Message)> CreateCategoryAsync(int eventId, CreateCategoryDto dto);
        Task<(bool Success, string Message)> EnrolAsync(int categoryId);
        Task<List<EnrolmentDto>> GetMyEnrolmentsAsync();
    }
}