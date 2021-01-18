using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

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
        public async Task<Game> GetGame(int id)
        {
            return await _logic.GetGameById(id);
        }

        [HttpPost]
        public async Task<Game> CreateGame()
        {
            return await _logic.CreateGame();
        }

        [HttpPut("edit/{id}")]
        public async Task<Game> EditGame(int id)
        {
            return await _logic.EditGame(id);
        }

    }
}
