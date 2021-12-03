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
    public class GameStatesController : ControllerBase
    {
        private readonly MPGCContext _context;

        public GameStatesController(MPGCContext context)
        {
            _context = context;
        }

        // GET: api/GameStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameState>>> GetGameStates()
        {
            return await _context.GameStates.ToListAsync();
        }

        // GET: api/GameStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameState>> GetGameState(int id)
        {
            var gameState = await _context.GameStates.FindAsync(id);

            if (gameState == null)
            {
                return NotFound();
            }

            return gameState;
        }

        // PUT: api/GameStates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameState(int id, GameState gameState)
        {
            if (id != gameState.IdgameState)
            {
                return BadRequest();
            }

            _context.Entry(gameState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameStateExists(id))
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

        // POST: api/GameStates
        [HttpPost]
        public async Task<ActionResult<GameState>> PostGameState(GameState gameState)
        {
            _context.GameStates.Add(gameState);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GameStateExists(gameState.IdgameState))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGameState", new { id = gameState.IdgameState }, gameState);
        }

        // DELETE: api/GameStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameState(int id)
        {
            var gameState = await _context.GameStates.FindAsync(id);
            if (gameState == null)
            {
                return NotFound();
            }

            _context.GameStates.Remove(gameState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameStateExists(int id)
        {
            return _context.GameStates.Any(e => e.IdgameState == id);
        }
    }
}
