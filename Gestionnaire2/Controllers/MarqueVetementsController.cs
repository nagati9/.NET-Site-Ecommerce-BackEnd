using Gestionnaire2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarqueVetementsController : ControllerBase
    {
        private AppDbContext _context;

        public MarqueVetementsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tests
        [HttpGet]
        public IActionResult Get()
        {
            var tests = _context.MarqueVetements.ToList();
            return Ok(tests);
        }
    }
}
