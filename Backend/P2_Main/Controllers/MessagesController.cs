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
        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _logic.GetMessages();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(Guid id)
        {
            return await _logic.GetMessageById(id);
        }

        [HttpGet("Sender/{id}")]
        public async Task<IEnumerable<Message>> GetMessagesBySenderById(Guid id)
        {
            return await _logic.GetMessagesBySenderById(id);
        }

        [HttpPost("Send")]
        public async Task<ActionResult<Message>> SendMessage(NewMessageDto newMessageDto)
        {
            Message message = await _logic.CreateNewMessage(newMessageDto);
            Message sent = await _logic.SendMessage(message);
            if (sent == null)
            {
                _logger.LogInformation("Bad Request");
                return BadRequest("Message was not sent");
            }
            return sent;
        }

        [HttpPost("Send/Carpool")]
        public async Task<ActionResult<Message>> SendCarpool(CarpoolingDto carpoolDto)
        {
            Message sent = await _logic.SendCarpool(carpoolDto);
            if (sent == null)
            {
                _logger.LogInformation("Bad Request");
                return BadRequest("Message was not sent");
            }
            return sent;
        }

        [HttpPost("Send/Reply")]
        public async Task<ActionResult<Message>> SendReply(ReplyDto replyDto)
        {
            Message sent = await _logic.SendReply(replyDto);
            if (sent == null)
            {
                _logger.LogInformation("Bad Request");
                return BadRequest("Message was not sent");
            }
            return sent;
        }

        [HttpGet("RecipientLists")]
        public async Task<IEnumerable<RecipientList>> GetRecipientLists()
        {
            return await _logic.GetRecipientLists();
        }

        [HttpGet("RecipientLists/{id}")]
        public async Task<ActionResult<RecipientList>> GetRecipientList(Guid id)
        {
            return await _logic.GetRecipientListById(id);
        }

        [HttpGet("Inboxes/{id}")]
        public async Task<IEnumerable<UserInbox>> GetUserInboxes(Guid id)
        {
            return await _logic.GetUserInbox(id);
        }

    }
}
