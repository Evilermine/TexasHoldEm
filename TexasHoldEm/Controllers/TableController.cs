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

        public TableController(PokerManagerContext context)
        {
            _context = context;
        }

        [HttpGet("{username}")]
        [Route("~/api/GetCard/{username}")]
        public IActionResult GetCard([FromRoute(Name ="username")] string username)
        {
            table = new Table(username);
                String[] cards = new String[2];
                table.DistributeToPlayer(2);
                cards = table.GetCardsByPlayer(username);
                return Ok(cards);
        }

        [HttpPost]
        [Route("~/api/action")]
        public IActionResult onAction([FromBody] ActionViewModel action)
        {
            if(action.action > 0)
            {
                table.onBid(action.action);
                table.nextTurn();
                return new OkResult();
            }
            if (action.action == 0)
            {
                table.nextTurn();
                return new OkResult();
            }
            if(action.action == -1)
            {
                table.onPlayerFold();
                table.nextTurn();
                return new OkResult();
            }

            return Content("Unknown client error");
        }

        [HttpGet]
        [Route("~/api/getPlayerTurn")]
        public IActionResult getPlayerTurn()
        {
            return Ok(table.getCurrentPlayerTurn());
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
        [Route("~/api/addToRiver")]
        public IActionResult addToRiver()
        {
            List<Card> card;
            card = table.DistributeRiver(1);
            table.Discard(1);

            return Ok(card);
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