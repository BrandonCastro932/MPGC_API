using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPGC_API.Models;
using MyStuffAPI_BrandonCastro.Attributes;

namespace MPGC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class UsersController : ControllerBase
    {
        private readonly MPGCContext _context;

        public UsersController(MPGCContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.SingleAsync(g => g.Iduser == id);
            
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("UserGames/{id}")]
        public async Task<ICollection<UserGame>> UserGames(int id)
        {
            var user = await _context.Users.SingleAsync(g => g.Iduser == id);
            _context.Entry(user).Collection(u => u.UserGames).Query().Include(u => u.IdgameNavigation).ThenInclude(u => u.IdgenreNavigation).Load();

            var userGames = user.UserGames;
            if (user.UserGames == null)
            {
                return (ICollection<UserGame>)NoContent();
            }

            return user.UserGames;
        }

        // GET: api/Users/5
        [HttpGet("Login")]
        public async Task<ActionResult<User>> Login(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(g => g.Username == username && g.Password == password);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Iduser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Iduser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Iduser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Iduser == id);
        }
    }
}
