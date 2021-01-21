using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataTransfer;

namespace P2_Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly LogicClass _logic;
        private readonly ILogger<GamesController> _logger;

        public GamesController(LogicClass logic, ILogger<GamesController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Game>> GetGames()
        {
            return await _logic.GetGames();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            return await _logic.GetGameById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Game>> CreateGame(CreateGameDto game)
        {
            return await _logic.CreateGame(game);
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult<Game>> EditGame(int id, EditGameDto editGameDto)
        {
            return await _logic.EditGame(id, editGameDto);
        }
    }
}
