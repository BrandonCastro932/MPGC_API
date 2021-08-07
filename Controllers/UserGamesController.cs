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
    public class UserGamesController : ControllerBase
    {
        private readonly MPGCContext _context;

        public UserGamesController(MPGCContext context)
        {
            _context = context;
        }

        // GET: api/UserGames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGame>>> GetUserGames()
        {
            return await _context.UserGames.ToListAsync();
        }

        // GET: api/UserGames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGame>> GetUserGame(int id)
        {
            var userGame = await _context.UserGames.FindAsync(id);

            if (userGame == null)
            {
                return NotFound();
            }

            return userGame;
        }

        // PUT: api/UserGames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserGame(int id, UserGame userGame)
        {
            if (id != userGame.IduserGame)
            {
                return BadRequest();
            }

            _context.Entry(userGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGameExists(id))
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

        // POST: api/UserGames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserGame>> PostUserGame(UserGame userGame)
        {
            var usergame = await _context.UserGames.SingleOrDefaultAsync(g => g.Idgame == userGame.Idgame && g.Iduser == userGame.Iduser);
            try
            {
                if (usergame != null)
                {
                    _context.UserGames.Remove(usergame);
                    _context.UserGames.Add(userGame);
                    await _context.SaveChangesAsync();
                }
                
                    _context.UserGames.Add(userGame);
                    await _context.SaveChangesAsync();
               
            }
            catch (DbUpdateException ex)
            {
                if (UserGameExists(userGame.IduserGame))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserGame", new { id = userGame.IduserGame }, userGame);
        }

        // DELETE: api/UserGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserGame(int id)
        {
            var userGame = await _context.UserGames.FindAsync(id);
            if (userGame == null)
            {
                return NotFound();
            }

            _context.UserGames.Remove(userGame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserGameExists(int id)
        {
            return _context.UserGames.Any(e => e.IduserGame == id);
        }
    }
}
