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
            table = new Table();
        }

        [HttpGet]
        [Route("~/api/GetCard")]
        public string GetCard()
        {
            // return table.deck.first.getImage();
            return "";
        }

        [HttpPost("{id}")]
        [Route("~/api/action")]
        public IActionResult onAction([FromBody] ActionViewModel action)
        {
            if(action.action > 0)
            {
                try
                {
                    return RedirectToAction("test", "asdf");
                }
                catch(Exception ex)
                {
                    return new NotFoundResult();
                }
            }
            return new NotFoundResult();
        }
    }
}