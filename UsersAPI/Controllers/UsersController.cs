using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Data;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Users>>> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Users>>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
           if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<Users>>> AddUser(Users request)
        {

            _context.Users.Add(request);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Users>>> UpdateUser(Users request)
        {

            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            user.Age = request.Age;
            user.Firstname = request.Firstname;
            user.LastName = request.LastName; 
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Users>>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }
    }
}
