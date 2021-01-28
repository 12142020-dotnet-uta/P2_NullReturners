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
        private readonly Mapper _mapper;
        private readonly ILogger<AccountController> _logger;
        public AccountController(LogicClass logic, Mapper mapper, ILogger<AccountController> logger)
        {
            _logic = logic;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserLoggedInDto>> Register(CreateUserDto createUser)
        {
            if (await _logic.UserExists(createUser.UserName, createUser.Email))
            {
                return BadRequest("Username is taken");
            }
            return await _logic.RegisterUser(createUser);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserLoggedInDto>> Login(LoginDto loginDto)
        {
            Task<User> loginUser = _logic.LoginUser(loginDto);
            if (loginUser.Result == null)
            {
                return Unauthorized("Invalid username");
            }
            UserLoggedInDto user = await _logic.CheckPassword(loginUser, loginDto);
            if (user == null)
            {
                return Unauthorized("Invalid password");
            }
            return user;
        }
    }
}
