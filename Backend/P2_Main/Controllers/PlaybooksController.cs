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

        // get play does not exist yet
        //[HttpGet("plays/{id}")]
        //public async Task<ActionResult<Play>> GetPlay(int id)
        //{
        //    return await _logic.GetPlay(id);
        //}

        // create playbook does not exist
        //[HttpPost]
        //public async Task<ActionResult<Playbook>> CreatePlaybook(int TeamId)
        //{
        //    return await _logic.CreatePlaybook(TeamId);
        //}

        // JOSH: Create CreatePlayDto DTO
        // PlaybookId, Playname, Description, drawnPlay
        // createplay method does not exist either
        //[HttpPost("plays")]
        //public async Task<ActionResult<Play>> CreatePlay(PlayDto createPlay)
        //{
        //    return await _logic.CreatePlay(createPlay);
        //}

        // EditPlay method does not exist
        //[HttpPut("plays/edit/{id}")]
        //public async Task<ActionResult<Play>> EditPlay(int PlayID, PlayDto createPlay)
        //{
        //    return await _logic.EditPlay(PlayID, createPlay);
        //}


        // DeletePlaybook method does not exist
        //[HttpDelete("/delete/{id}")]
        //public async Task<ActionResult<Playbook>> DeletePlaybook(int id)
        //{
        //    return await _logic.DeletePlaybook(id);
        //}

        // DeletePlay method does not exist
        //[HttpDelete("plays/delete/{id}")]
        //public async Task<ActionResult<Play>> DeletePlay(int id)
        //{
        //    return await _logic.DeletePlay(id);
        //}

    }
}
