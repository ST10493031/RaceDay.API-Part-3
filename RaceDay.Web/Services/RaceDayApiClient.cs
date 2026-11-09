using System.Text;
using System.Text.Json;
using RaceDay.Web.Models;
namespace RaceDay.Web.Services
{
    public class RaceDayApiClient : IRaceDayApiClient
    {
        public async Task<List<dynamic>> GetEventEnrolmentsAsync(int eventId)
        {
            AttachToken();
            var res = await _http.GetAsync($"/api/events/{eventId}/enrolments");
            return res.IsSuccessStatusCode ? JsonSerializer.Deserialize<List<dynamic>>(await res.Content.ReadAsStringAsync(), _json) ?? new() : new();
        }

        public async Task<(bool, string)> CreateResultAsync(int enrolmentId, CreateResultDto dto)
        {
            AttachToken();
            var res = await _http.PostAsync($"/api/enrolments/{enrolmentId}/results", new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
            return res.IsSuccessStatusCode ? (true, "Saved") : (false, await res.Content.ReadAsStringAsync());
        }

        public async Task<List<dynamic>> GetEventResultsAsync(int eventId)
        {
            var res = await _http.GetAsync($"/api/events/{eventId}/results");
            return res.IsSuccessStatusCode ? JsonSerializer.Deserialize<List<dynamic>>(await res.Content.ReadAsStringAsync(), _json) ?? new() : new();
        }
        private readonly HttpClient _http;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

        public RaceDayApiClient(HttpClient http, IHttpContextAccessor contextAccessor)
        {
            _http = http;
            _contextAccessor = contextAccessor;
        }

        private void AttachToken()
        {
            var token = _contextAccessor.HttpContext?.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<(bool, string)> RegisterAsync(RegisterDto dto)
        {
            var res = await _http.PostAsync("/api/Auth/register", new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
            return res.IsSuccessStatusCode ? (true, "Success") : (false, await res.Content.ReadAsStringAsync());
        }

        public async Task<(bool, LoginResponseDto?)> LoginAsync(LoginDto dto)
        {
            var res = await _http.PostAsync("/api/Auth/login", new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
            if (!res.IsSuccessStatusCode) return (false, null);
            return (true, JsonSerializer.Deserialize<LoginResponseDto>(await res.Content.ReadAsStringAsync(), _json));
        }

        public async Task<List<EventDto>> GetEventsAsync()
        {
            AttachToken();
            var res = await _http.GetAsync("/api/Events");
            return res.IsSuccessStatusCode ? JsonSerializer.Deserialize<List<EventDto>>(await res.Content.ReadAsStringAsync(), _json) ?? new() : new();
        }

        public async Task<EventDto?> GetEventAsync(int id)
        {
            AttachToken();
            var res = await _http.GetAsync($"/api/Events/{id}");
            return res.IsSuccessStatusCode ? JsonSerializer.Deserialize<EventDto>(await res.Content.ReadAsStringAsync(), _json) : null;
        }

        public async Task<(bool, int)> CreateEventAsync(EventDto dto)
        {
            AttachToken();
            var res = await _http.PostAsync("/api/Events", new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
            if (!res.IsSuccessStatusCode) return (false, 0);
            var ev = JsonSerializer.Deserialize<EventDto>(await res.Content.ReadAsStringAsync(), _json);
            return (true, ev!.EventID);
        }

        public async Task<(bool, string)> UploadBannerAsync(int eventId, Stream fileStream, string fileName)
        {
            AttachToken();
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(fileStream), "file", fileName);
            var res = await _http.PostAsync($"/api/Events/{eventId}/banner", content);
            return res.IsSuccessStatusCode ? (true, "Uploaded") : (false, await res.Content.ReadAsStringAsync());
        }

        public async Task<(bool, string)> CreateCategoryAsync(int eventId, CreateCategoryDto dto)
        {
            AttachToken();
            var res = await _http.PostAsync($"/api/events/{eventId}/categories", new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
            return res.IsSuccessStatusCode ? (true, "Created") : (false, await res.Content.ReadAsStringAsync());
        }

        public async Task<(bool, string)> EnrolAsync(int categoryId)
        {
            AttachToken();
            var res = await _http.PostAsync($"/api/categories/{categoryId}/enrolments", null);
            return res.IsSuccessStatusCode ? (true, "Enroled") : (false, await res.Content.ReadAsStringAsync());
        }

        public async Task<List<EnrolmentDto>> GetMyEnrolmentsAsync()
        {
            AttachToken();
            var res = await _http.GetAsync("/api/enrolments/me");
            return res.IsSuccessStatusCode ? JsonSerializer.Deserialize<List<EnrolmentDto>>(await res.Content.ReadAsStringAsync(), _json) ?? new() : new();
        }
    }
}