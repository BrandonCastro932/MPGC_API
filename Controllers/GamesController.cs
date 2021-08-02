using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPGC_API.Filter;
using MPGC_API.Helpers;
using MPGC_API.Models;
using MPGC_API.Services;
using MyStuffAPI_BrandonCastro.Attributes;

namespace MPGC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiKey]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly MPGCContext _context;
        private readonly IUriService uriService;

        public GamesController(MPGCContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
        }

        // GET: api/Games
        [HttpGet]
        //Añadir esto en caso de paginar [FromQuery] PaginationFilter filter
        public async Task<ActionResult<IEnumerable<Game>>> GetGames([FromQuery] PaginationFilter filter)
        {
           // return await _context.Games.Include(p => p.IdgenreNavigation).ToListAsync();

            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Games
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .Include(p => p.IdgenreNavigation)
                .ToListAsync();
            var totalRecords = await _context.Games.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Game>(pagedData, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);

            /* Para paginar
           
             */
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games.SingleAsync(g => g.Idgame == id);

            _context.Entry(game).Collection(g => g.GameMovies).Load();
            _context.Entry(game).Collection(g => g.GameScreenshots).Load();
            _context.Entry(game).Collection(g => g.GamePlatforms).Query().Include(d => d.PlatformsIdplatformNavigation).Load();
            _context.Entry(game).Reference(g => g.IdgenreNavigation).Load();

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Idgame)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.Games.Add(game);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GameExists(game.Idgame))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGame", new { id = game.Idgame }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Idgame == id);
        }
    }
}
