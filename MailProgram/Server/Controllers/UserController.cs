using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailProgram.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext dataContext)
        {
            _context = dataContext;
        }

        /**
         * 
         * HTTP Methods
         * 
         */

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetSingleUser(int id)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            if(user == null)
            {
                return BadRequest("User cannot be null");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(User user, int id)
        {
            User? databaseUser = await _context.Users.FirstOrDefaultAsync(u =>u.UserId == id);

            if (user == null)
            {
                return BadRequest("User cannot be null");
            } else if (databaseUser == null)
            {
                return NotFound("User not found in database");
            }

            databaseUser.Name = user.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            User? databaseUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (databaseUser == null)
            {
                return NotFound("User not found in database");
            }

            _context.Users.Remove(databaseUser);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
