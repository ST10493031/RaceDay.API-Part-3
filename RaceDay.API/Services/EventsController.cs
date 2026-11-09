using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Services;
// Add this using if your Event entity is in a different namespace
using RaceDay.API.Models; // <-- Add this line if your Event class is in RaceDay.API.Models

[Authorize(Roles = "Organiser")]
public class EventsController : ControllerBase
{
    private readonly DbContext _context;

    public EventsController(DbContext context)
    {
        _context = context;
    }

    [HttpPost("{id}/banner")]
    public async Task<IActionResult> UploadBanner(int id, IFormFile file, [FromServices] BlobService blobService)
    {
        // Use Set<Event>() to access the Events DbSet
        var ev = await _context.Set<Event>().FindAsync(id);
        if (ev == null) return NotFound();
        ev.BannerUrl = await blobService.UploadFileAsync(file);
        await _context.SaveChangesAsync();
        return Ok(new { BannerUrl = ev.BannerUrl });
    }
}