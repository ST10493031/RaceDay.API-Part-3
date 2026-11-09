using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;

namespace RaceDay.Web.Controllers
{
    public class EnrolmentsController : Controller
    {
        private readonly IRaceDayApiClient _api;
        public EnrolmentsController(IRaceDayApiClient api) => _api = api;

        public async Task<IActionResult> MyEnrolments()
        {
            if (HttpContext.Session.GetString("UserRole") != "Participant") return RedirectToAction("Login", "Account");
            var enrolments = await _api.GetMyEnrolmentsAsync();
            return View(enrolments);
        }
        public async Task<IActionResult> MyResults()
        {
            if (HttpContext.Session.GetString("UserRole") != "Participant") return RedirectToAction("Login", "Account");

            // In a real app, you'd call a specific API endpoint for the participant's results.
            // For simplicity, we'll fetch events and assume they want to see the public results board.
            var events = await _api.GetEventsAsync();
            return View(events);
        }
        public async Task<IActionResult> EventResults(int eventId)
        {
            var results = await _api.GetEventResultsAsync(eventId);
            ViewBag.EventId = eventId;
            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> Enrol(int categoryId, int eventId)
        {
            var result = await _api.EnrolAsync(categoryId);
            if (result.Item1) TempData["Success"] = "Successfully enrolled!";
            else TempData["Error"] = result.Item2;

            return RedirectToAction("Details", "Events", new { id = eventId });
        }
    }
}