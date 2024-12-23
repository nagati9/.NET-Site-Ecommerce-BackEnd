using Gestionnaire2.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tests
        [HttpGet]
        public IActionResult Get()
        {
            var tests = _context.Tests.ToList();
            return Ok(tests);
        }

        // GET api/Tests/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var test = _context.Tests.Find(id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }


        // POST api/<TestsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
