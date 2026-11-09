using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;      
using RaceDay.API.DTOs;
using RaceDay.API.Models;
using RaceDay.API.Services;
using System.Security.Claims;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        
        private readonly RaceDayDbContext _context;

       
        public EventsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/events
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _context.Events.Include(e => e.Categories).ToListAsync();
            return Ok(events);
        }

        // GET: api/events/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var ev = await _context.Events
                .Include(e => e.Categories)
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (ev == null) return NotFound("No event with that ID.");
            return Ok(ev);
        }

        // POST: api/events
        [Authorize(Roles = "Organiser")]
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var newEvent = new Event
            {
                UserID = userId,
                EventName = dto.EventName,
                Description = dto.Description,
                EventDate = dto.EventDate,
                Location = dto.Location,
                EventType = dto.EventType
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.EventID }, newEvent);
        }

        // POST: api/events/{id}/banner
        [Authorize(Roles = "Organiser")]
        [HttpPost("{id}/banner")]
        public async Task<IActionResult> UploadBanner(int id, IFormFile file, [FromServices] BlobService blobService)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();

            ev.BannerUrl = await blobService.UploadFileAsync(file);
            await _context.SaveChangesAsync();

            return Ok(new { BannerUrl = ev.BannerUrl });
        }
    }
}