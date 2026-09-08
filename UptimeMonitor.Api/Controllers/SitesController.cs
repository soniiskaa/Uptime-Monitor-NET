using Microsoft.EntityFrameworkCore;
using UptimeMonitor.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace UptimeMonitor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SitesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SitesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GatSites()
        {
            var sites = await _context.Sites.ToListAsync();
            return Ok(sites);
        }

        [HttpPost]
        public async Task<IActionResult> AddSite([FromBody] string url)
        {
            var newSite = new MonitoredSite { Url = url };

            await _context.Sites.AddAsync(newSite);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Site is successfully added" });
        }

    }
}
