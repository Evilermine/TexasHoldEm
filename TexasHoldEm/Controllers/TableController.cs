using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexasHoldEm.Services;
using TexasHoldEm.Models;
using TexasHoldEm.ViewModels;

namespace TexasHoldEm.Controllers
{
    [Produces("application/json")]
    public class TableController : Controller
    {
        private Table table;
        private readonly PokerManagerContext _context;
        private PlayersController pController;

        public TableController(PokerManagerContext context)
        {
            this._context = context;
        }

        [HttpGet("{id}, name=id")]
        [Route("~/api/GetCard")]
        public IActionResult GetCard([FromRoute] string id)
        {
                String[] cards = new String[2];
                table.DistributeToPlayer(2);
                cards = table.GetCardsByPlayer(id);

            return Content("test");
                //return Ok(cards);
        }

        [HttpPost("{id}")]
        [Route("~/api/action")]
        public IActionResult onAction([FromBody] ActionViewModel action)
        {
            if(action.action > 0)
            {
            }
            else if(action.action == 0)
            {
            }
            return new NotFoundResult();
        }

        [HttpPost]
        [Route("~/api/addToTable")]
        public IActionResult addToTable([FromBody] UserViewModel p)
        {
            try
            {
                if (table == null)
                    table = new Table(p.username);
                else
                    table.AddPlayer(p.username);

                return new OkResult();
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("~/api/removeFromTable")]
        public IActionResult removeFromTable([FromBody] UserViewModel p)
        {
            Boolean IsNotEmpty;
            IsNotEmpty = table.RemovePlayer(p.username);

            if (!IsNotEmpty)
                table = null;

            return new OkResult();
        }

        [HttpGet]
        [Route("~/api/getRiver")]
        public IActionResult getBaseRiver()
        {
            List<Card> cards;
            cards = table.DistributeRiver(3);
            table.Discard(1);

            return Ok(cards);
        }

        [HttpGet]
        [Route("~/api/getWinner")]
        public IActionResult getWinner()
        {
            List<Seat> winners;
            List<String> win_user = new List<String>();
            winners = table.CalculateWinner();
            table.EndRound();

            foreach (Seat seat in winners)
            {
                win_user.Add(seat.GetPlayerName());
            }

            return Ok(win_user.ToArray());
        }
    }
}