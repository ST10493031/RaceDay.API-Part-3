using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;
namespace RaceDay.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRaceDayApiClient _api;
        public HomeController(IRaceDayApiClient api) => _api = api;

        public async Task<IActionResult> Index() => View(await _api.GetEventsAsync());
    }
}