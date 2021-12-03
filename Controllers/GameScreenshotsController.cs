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
    public class GameScreenshotsController : ControllerBase
    {
        private readonly MPGCContext _context;

        public GameScreenshotsController(MPGCContext context)
        {
            _context = context;
        }

        // GET: api/GameScreenshots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameScreenshot>>> GetGameScreenshots()
        {
            return await _context.GameScreenshots.ToListAsync();
        }

        // GET: api/GameScreenshots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameScreenshot>> GetGameScreenshot(int id)
        {
            var gameScreenshot = await _context.GameScreenshots.FindAsync(id);

            if (gameScreenshot == null)
            {
                return NotFound();
            }

            return gameScreenshot;
        }

        // PUT: api/GameScreenshots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameScreenshot(int id, GameScreenshot gameScreenshot)
        {
            if (id != gameScreenshot.Idscreenshot)
            {
                return BadRequest();
            }

            _context.Entry(gameScreenshot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameScreenshotExists(id))
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

        // POST: api/GameScreenshots
        [HttpPost]
        public async Task<ActionResult<GameScreenshot>> PostGameScreenshot(GameScreenshot gameScreenshot)
        {
            _context.GameScreenshots.Add(gameScreenshot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameScreenshot", new { id = gameScreenshot.Idscreenshot }, gameScreenshot);
        }

        // DELETE: api/GameScreenshots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameScreenshot(int id)
        {
            var gameScreenshot = await _context.GameScreenshots.FindAsync(id);
            if (gameScreenshot == null)
            {
                return NotFound();
            }

            _context.GameScreenshots.Remove(gameScreenshot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameScreenshotExists(int id)
        {
            return _context.GameScreenshots.Any(e => e.Idscreenshot == id);
        }
    }
}
