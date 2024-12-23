using Gestionnaire2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class TypeVetementsController : ControllerBase
    {
        private AppDbContext _context;

        public TypeVetementsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tests
        [HttpGet]
        public IActionResult Get()
        {
            var tests = _context.TypeVetements.ToList();
            return Ok(tests);
        }
    }
}
