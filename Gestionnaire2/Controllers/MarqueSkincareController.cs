using Gestionnaire2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarqueSkincareController : ControllerBase
    {
        private AppDbContext _context;

        public MarqueSkincareController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tests
        [HttpGet]
        public IActionResult Get()
        {
            var tests = _context.MarqueSkincares.ToList();
            return Ok(tests);
        }
    }
}
