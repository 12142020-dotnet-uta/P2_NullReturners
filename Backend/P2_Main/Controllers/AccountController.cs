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
    public class AccountController : ControllerBase
    {
        private readonly LogicClass _logic;
        private readonly ILogger<AccountController> _logger;

        public AccountController(LogicClass logic, ILogger<AccountController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(CreateUserDto createUser)
        {
            return await _logic.RegisterUser(createUser);
        }

    }
}
