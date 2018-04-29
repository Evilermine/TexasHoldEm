using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TexasHoldEm.Models;
using TexasHoldEm.ViewModels;

namespace TexasHoldEm.Controllers
{
    [Produces("application/json")]
    [Route("~/api/Players")]
    public class PlayersController : Controller
    {
        private PokerManagerContext _context;

        public PlayersController(PokerManagerContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        [Route("~/api/GetAllPlayers")]
        public IEnumerable<Players> GetPlayers()
        {
            return _context.Players;
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        [Route("~/api/GetPlayer")]
        public async Task<IActionResult> GetPlayers([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var players = await _context.Players.SingleOrDefaultAsync(m => m.Username == id);

            if (players == null)
            {
                return NotFound();
            }

            return Ok(players);
        }

        
        // PUT: api/Players/5
        [HttpPut("{id}")]
        [Route("~/api/EditPlayer")]
        public async Task<IActionResult> PutPlayers([FromRoute] string id, [FromBody] Players players)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != players.Username)
            {
                return BadRequest();
            }

          
            try
            {
                players.Wallet = 12000;
                _context.Players.Attach(players);
                _context.Entry(players).Property(p => p.Wallet).IsModified = true;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayersExists(id))
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

        // POST: api/Players
        [HttpPost]
        [Route("~/api/InsertPlayer")]
        public async Task<IActionResult> PostPlayers([FromBody] Players players)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Players.Add(players);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlayersExists(players.Username))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlayers", new { id = players.Username }, players);
        }
        
        [HttpPost]
        [Route("~/api/login")]
        public IActionResult login([FromBody] UserCredentialsViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
               Players p = _context.Players.SingleOrDefault(m => m.Username == model.username);

                if (p == null)
                    return NotFound(p);

                if (p.Password != model.password)
                    return new UnauthorizedResult();

                return Json(p);
            }
            catch (Exception ex)
            {
                return new UnauthorizedResult();
            }
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        [Route("~/api/DeletePlayer")]
        public async Task<IActionResult> DeletePlayers([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var players = await _context.Players.SingleOrDefaultAsync(m => m.Username == id);
            if (players == null)
            {
                return NotFound();
            }

            _context.Players.Remove(players);
            await _context.SaveChangesAsync();

            return Ok(players);
        }

        private bool PlayersExists(string id)
        {
            return _context.Players.Any(e => e.Username == id);
        }
        
        [HttpPost]
        [Route("~/api/BidPlayer")]
        public IActionResult BidPlayer([FromBody]ActionViewModel action)
        {
            if (!PlayersExists(action.user))
                return new NotFoundObjectResult(action.user);

            Players player = _context.Players.SingleOrDefault(m => m.Username == action.user);
            player.Wallet = player.Wallet - action.action;

            _context.Attach(player);
            _context.Entry(player).Property(p => p.Wallet);
            _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}