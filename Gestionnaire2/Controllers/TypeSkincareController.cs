using Gestionnaire2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeSkincareController : ControllerBase
    {
        private AppDbContext _context;

        public TypeSkincareController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tests
        [HttpGet]
        public IActionResult Get()
        {
            var tests = _context.TypeSkincare.ToList();
            return Ok(tests);
        }
    }
}
