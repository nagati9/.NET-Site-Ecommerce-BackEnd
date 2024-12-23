using Gestionnaire2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetementsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VetementsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tests
        [HttpGet]
        public IActionResult Get()
        {
            var vetements = _context.Vetements
       .Include(v => v.Type)
       .Include(v => v.Marque)
       .ToList();

            return Ok(vetements);
        }
    }
}
