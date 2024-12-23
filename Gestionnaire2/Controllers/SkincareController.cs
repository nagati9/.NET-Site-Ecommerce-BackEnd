using Gestionnaire2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkincareController : ControllerBase
    {
        private AppDbContext _context;

        public SkincareController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tests
        [HttpGet]
        public IActionResult Get()
        {
            var tests = _context.Skincares.Include(s => s.Marque).Include(s => s.Type).ToList();
            return Ok(tests);
        }
    }
}
