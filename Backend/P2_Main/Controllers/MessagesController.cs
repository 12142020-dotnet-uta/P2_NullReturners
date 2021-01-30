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
    public class MessagesController : ControllerBase
    {
        private readonly LogicClass _logic;
        private readonly ILogger<UsersController> _logger;
        public MessagesController(LogicClass logic, ILogger<UsersController> logger)
        {
            _logic = logic;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<Message>> GetUsers()
        {
            return await _logic.GetMessages();
        }

        [HttpPost("Send")]
        public async Task<ActionResult<Message>> SendMessage(NewMessageDto newMessageDto)
        {
            Message message = await _logic.CreateNewMessage(newMessageDto);
            bool sent = await _logic.SendMessage(message);
            if (sent == false)
            {
                return BadRequest("Message was not sent");
            }
            return message;
        }

        [HttpGet("RecipientLists")]
        public async Task<IEnumerable<RecipientList>> GetRecipientLists(Guid id)
        {
            return await _logic.GetRecipientLists();
        }

        [HttpGet("RecipientLists/{id}")]
        public async Task<ActionResult<RecipientList>> GetRecipientList(Guid id)
        {
            return await _logic.GetRecipientListById(id);
        }
    }
}
