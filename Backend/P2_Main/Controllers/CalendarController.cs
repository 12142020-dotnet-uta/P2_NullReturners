using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P2_Main.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ILogger<CalendarController> _logger;
        public CalendarController(ILogger<CalendarController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Calendar>> GetCalendar()
        {
            return await LogicClass.GetCalendar();
        }

        [HttpGet("events")]
        public async Task<IEnumerable<Event>> GetMyEvents()
        {
            return await LogicClass.GetMyEvents();
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(EventDto eventDto)
        {
            var ret = await LogicClass.CreateEvent(eventDto);
            if (ret == null)
            {
                return BadRequest("Event not created");
            }
            return ret;
        }

        [HttpPut("events/{id}")]
        public async Task<ActionResult<Event>> EditEvent(EventDto eventDto, string id)
        {
            return await LogicClass.EditEvent(eventDto, id);
        }

        [HttpDelete("events/{id}")]
        public async Task<ActionResult<string>> DeleteEvent(string id)
        {
            return await LogicClass.DeleteEvent(id);
        }
    }
}
