using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Models;
using RaceDay.Web.Services;

namespace RaceDay.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IRaceDayApiClient _apiClient;

		public AccountController(IRaceDayApiClient apiClient)
		{
			_apiClient = apiClient;
		}

		// GET: /Account/Login
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		// POST: /Account/Login
		[HttpPost]
		public async Task<IActionResult> Login(LoginDto dto)
		{
			if (!ModelState.IsValid) return View(dto);

			var result = await _apiClient.LoginAsync(dto);

			if (result.Success && result.Data != null)
			{
				// Store the token and role in the session
				HttpContext.Session.SetString("JWToken", result.Data.Token);
				HttpContext.Session.SetString("UserRole", result.Data.Role);

				// Redirect based on role
				if (result.Data.Role == "Organiser")
					return RedirectToAction("Index", "Home"); // We'll create a dashboard later

				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", "Invalid email or password.");
			return View(dto);
		}

		// GET: /Account/Register
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		// POST: /Account/Register
		[HttpPost]
		public async Task<IActionResult> Register(RegisterDto dto)
		{
			if (!ModelState.IsValid) return View(dto);

			var result = await _apiClient.RegisterAsync(dto);

			if (result.Success)
			{
				TempData["SuccessMessage"] = "Registration successful. Please log in.";
				return RedirectToAction(nameof(Login));
			}

			ModelState.AddModelError("", result.Message);
			return View(dto);
		}

		// POST: /Account/Logout
		[HttpPost]
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
	}
}