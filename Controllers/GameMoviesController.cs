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
    public class GameMoviesController : ControllerBase
    {
        private readonly MPGCContext _context;

        public GameMoviesController(MPGCContext context)
        {
            _context = context;
        }

        // GET: api/GameMovies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameMovie>>> GetGameMovies()
        {
            return await _context.GameMovies.ToListAsync();
        }

        // GET: api/GameMovies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameMovie>> GetGameMovie(int id)
        {
            var gameMovie = await _context.GameMovies.FindAsync(id);

            if (gameMovie == null)
            {
                return NotFound();
            }

            return gameMovie;
        }

        // PUT: api/GameMovies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameMovie(int id, GameMovie gameMovie)
        {
            if (id != gameMovie.IdgameMovie)
            {
                return BadRequest();
            }

            _context.Entry(gameMovie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameMovieExists(id))
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

        // POST: api/GameMovies
        [HttpPost]
        public async Task<ActionResult<GameMovie>> PostGameMovie(GameMovie gameMovie)
        {
            _context.GameMovies.Add(gameMovie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GameMovieExists(gameMovie.IdgameMovie))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGameMovie", new { id = gameMovie.IdgameMovie }, gameMovie);
        }

        // DELETE: api/GameMovies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameMovie(int id)
        {
            var gameMovie = await _context.GameMovies.FindAsync(id);
            if (gameMovie == null)
            {
                return NotFound();
            }

            _context.GameMovies.Remove(gameMovie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameMovieExists(int id)
        {
            return _context.GameMovies.Any(e => e.IdgameMovie == id);
        }
    }
}
