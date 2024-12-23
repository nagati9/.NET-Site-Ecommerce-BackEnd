using Gestionnaire2.Data;
using Gestionnaire2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParfumsController : ControllerBase
    {
        private AppDbContext _context;

        public ParfumsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tests
        [HttpGet]
        public IActionResult Get()
        {
            var parfums = _context.Parfums
                .Include(p => p.Marque)
                .Include(p => p.Type)   
                .ToList();

            return Ok(parfums);
        }

    }
}
