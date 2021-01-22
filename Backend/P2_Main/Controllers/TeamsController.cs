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
    public class TeamsController : ControllerBase
    {
        private readonly LogicClass _logic;
        private readonly ILogger<TeamsController> _logger;

        public TeamsController(LogicClass logic, ILogger<TeamsController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Team>> GetTeams()
        {
            _logger.LogTrace("GetTeams");
            return await _logic.GetTeams();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            _logger.LogTrace("GetTeam/id");
            return await _logic.GetTeamById(id);
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult<Team>> EditTeam(int id, EditTeamDto editTeam)
        {
            _logger.LogTrace("EditTeam/id");
            return await _logic.EditTeam(id, editTeam);
        }
    }
}
