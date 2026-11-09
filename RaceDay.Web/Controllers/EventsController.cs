using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Models;
using RaceDay.Web.Services;

namespace RaceDay.Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly IRaceDayApiClient _api;
        public EventsController(IRaceDayApiClient api) => _api = api;

        public async Task<IActionResult> Details(int id)
        {
            var ev = await _api.GetEventAsync(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser") return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventDto dto, IFormFile? bannerFile)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await _api.CreateEventAsync(dto);
            if (result.Item1)
            {
                if (bannerFile != null && bannerFile.Length > 0)
                {
                    using var stream = bannerFile.OpenReadStream();
                    await _api.UploadBannerAsync(result.Item2, stream, bannerFile.FileName);
                }
                TempData["Success"] = "Event created successfully!";
                return RedirectToAction("Index", "Organiser"); // Redirect to dashboard
            }
            ModelState.AddModelError("", "Failed to create event.");
            return View(dto);
        }
    }
}