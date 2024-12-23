using Gestionnaire2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeParfumsController : ControllerBase
    {
        private AppDbContext _context;

        public TypeParfumsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tests
        [HttpGet]
        public IActionResult Get()
        {
            var tests = _context.TypeParfums.ToList();
            return Ok(tests);
        }
    }
}
