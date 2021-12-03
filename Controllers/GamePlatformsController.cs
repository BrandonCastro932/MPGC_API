using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPGC_API.Models;

namespace MPGC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePlatformsController : ControllerBase
    {
        private readonly MPGCContext _context;

        public GamePlatformsController(MPGCContext context)
        {
            _context = context;
        }

        // GET: api/GamePlatforms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GamePlatform>>> GetGamePlatforms()
        {
            return await _context.GamePlatforms.ToListAsync();
        }

        // GET: api/GamePlatforms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GamePlatform>> GetGamePlatform(int id)
        {
            var gamePlatform = await _context.GamePlatforms.FindAsync(id);

            if (gamePlatform == null)
            {
                return NotFound();
            }

            return gamePlatform;
        }

        // PUT: api/GamePlatforms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGamePlatform(int id, GamePlatform gamePlatform)
        {
            if (id != gamePlatform.IdgamePlatform)
            {
                return BadRequest();
            }

            _context.Entry(gamePlatform).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamePlatformExists(id))
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

        // POST: api/GamePlatforms
        [HttpPost]
        public async Task<ActionResult<GamePlatform>> PostGamePlatform(GamePlatform gamePlatform)
        {
            _context.GamePlatforms.Add(gamePlatform);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGamePlatform", new { id = gamePlatform.IdgamePlatform }, gamePlatform);
        }

        // DELETE: api/GamePlatforms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGamePlatform(int id)
        {
            var gamePlatform = await _context.GamePlatforms.FindAsync(id);
            if (gamePlatform == null)
            {
                return NotFound();
            }

            _context.GamePlatforms.Remove(gamePlatform);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GamePlatformExists(int id)
        {
            return _context.GamePlatforms.Any(e => e.IdgamePlatform == id);
        }
    }
}
