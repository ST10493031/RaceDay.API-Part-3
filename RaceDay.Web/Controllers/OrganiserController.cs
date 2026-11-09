using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Models;
using RaceDay.Web.Services;

namespace RaceDay.Web.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly IRaceDayApiClient _api;
        public OrganiserController(IRaceDayApiClient api) => _api = api;

        public async Task<IActionResult> Index()
        {
            // 🛑 BLOCK PARTICIPANTS FROM ACCESSING THIS PAGE
            if (HttpContext.Session.GetString("UserRole") != "Organiser")
            {
                return RedirectToAction("Index", "Home"); // Send them back to Home
            }

            var events = await _api.GetEventsAsync();
            return View(events);
        }

        [HttpGet]
        public IActionResult CreateCategory(int eventId)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser") return RedirectToAction("Index", "Home");
            ViewBag.EventId = eventId;
            return View();
        }
        public async Task<IActionResult> EventEnrolments(int eventId)
        {
            if (HttpContext.Session.GetString("UserRole") != "Organiser") return RedirectToAction("Login", "Account");
            var enrolments = await _api.GetEventEnrolmentsAsync(eventId);
            ViewBag.EventId = eventId;
            return View(enrolments);
        }

        [HttpGet]
        public IActionResult CaptureResult(int enrolmentId, int eventId)
        {
            ViewBag.EnrolmentId = enrolmentId;
            ViewBag.EventId = eventId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CaptureResult(int enrolmentId, int eventId, CreateResultDto dto)
        {
            var result = await _api.CreateResultAsync(enrolmentId, dto);
            if (result.Item1)
            {
                TempData["Success"] = "Result captured!";
                return RedirectToAction("EventEnrolments", new { eventId = eventId });
            }
            ModelState.AddModelError("", result.Item2);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(int eventId, CreateCategoryDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await _api.CreateCategoryAsync(eventId, dto);
            if (result.Item1)
            {
                TempData["Success"] = "Category added!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Item2);
            return View(dto);
        }
    }
}