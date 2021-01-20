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

        // FIX THIS DANIEL
        // YES ALL OF THIS

        //[HttpGet]
        //public async Task<IEnumerable<Playbook>> GetPlaybooks()
        //{
        //    return await _logic.GetPlaybooks();
        //}

        //[HttpGet("{id}")]
        //public async Task<Playbook> GetPlaybook(int id)
        //{
        //    return await _logic.GetPlaybookById(id);
        //}


        //[HttpGet]
        //public async Task<IEnumerable<Play>> GetPlays()
        //{
        //    return await _logic.GetPlays();
        //}

        //[HttpGet("{id}")]
        //public async Task<Play> GetPlay(int id)
        //{
        //    return await _logic.GetPlay(id);
        //}


        //[HttpPost]
        //public async Task<Playbook> CreatePlaybook()
        //{
        //    return await _logic.CreatePlaybook();
        //}


        //[HttpPost]
        //public async Task<Play> CreatePlay(int playBookId)
        //{
        //    return await _logic.CreatePlay(playBookId);
        //}


        //[HttpPut("/edit/{id}")]
        //public async Task<Playbook> EditPlaybook(int id)
        //{
        //    return await _logic.EditPlaybook(id);
        //}

        //[HttpPut("/edit/{id}")]
        //public async Task<Play> EditPlay(int id)
        //{
        //    return await _logic.EditPlay(id);
        //}

        //[HttpDelete("/delete/{id}")]
        //public async Task<Playbook> DeletePlaybook(int id)
        //{
        //    return await _logic.DeletePlaybook(id);
        //}

        //[HttpDelete("/delete/{id}")]
        //public async Task<Play> DeletePlay(int id)
        //{
        //    return await _logic.DeletePlay(id);
        //}

    }
}
