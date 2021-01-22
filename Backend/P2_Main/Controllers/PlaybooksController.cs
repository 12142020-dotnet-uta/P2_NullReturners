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
    public class PlaybooksController : ControllerBase
    {
        private readonly LogicClass _logic;
        private readonly ILogger<PlaybooksController> _logger;

        public PlaybooksController(LogicClass logic, ILogger<PlaybooksController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Playbook>> GetPlaybooks()
        {
            return await _logic.GetPlaybooks();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Playbook>> GetPlaybook(int id)
        {
            return await _logic.GetPlaybookById(id);
        }

        [HttpGet("plays")]
        public async Task<IEnumerable<Play>> GetPlays()
        {
            return await _logic.GetPlays();
        }

        [HttpGet("plays/{id}")]
        public async Task<ActionResult<Play>> GetPlay(int id)
        {
            return await _logic.GetPlayById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Playbook>> CreatePlaybook(int teamId)
        {
            return await _logic.CreatePlaybook(teamId);
        }

        [HttpPost("plays")]
        public async Task<ActionResult<Play>> CreatePlay(PlayDto createPlay)
        {
            return await _logic.CreatePlay(createPlay);
        }

        [HttpPut("plays/edit/{id}")]
        public async Task<ActionResult<Play>> EditPlay(int PlayID, PlayDto createPlay)
        {
            return await _logic.EditPlay(PlayID, createPlay);
        }

        [HttpDelete("/delete/{id}")]
        public async Task<ActionResult<Playbook>> DeletePlaybook(int id)
        {
            return await _logic.DeletePlaybook(id);
        }

        [HttpDelete("plays/delete/{id}")]
        public async Task<ActionResult<Play>> DeletePlay(int id)
        {
            return await _logic.DeletePlay(id);
        }
    }
}
