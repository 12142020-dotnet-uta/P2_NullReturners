using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataTransfer;

namespace P2_Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LogicClass _logic;
        private readonly ILogger<UsersController> _logger;

        public UsersController(LogicClass logic, ILogger<UsersController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            _logger.LogTrace("GetUsers");
            return await _logic.GetUsers();
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserDto createUser)
        {
            _logger.LogTrace("CreateUser");
            return await _logic.CreateUser(createUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            _logger.LogTrace("GetUser/id");
            return await _logic.GetUserById(id);
        }

        [HttpGet("roles")]
        public async Task<IEnumerable<Role>> GetRoles()
        {
            _logger.LogTrace("GetRoles");
            return await _logic.GetRoles();
        }

        [HttpGet("roles/{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            _logger.LogTrace("GetRole/id");
            return await _logic.GetRoleById(id);
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult<User>> EditUser(Guid id, EditUserDto editedUser)
        {
            _logger.LogTrace("EditUser/id");
            return await _logic.EditUser(id, editedUser);
        }

        [HttpPut("coach/edit/{id}")]
        public async Task<ActionResult<User>> CoachEditUser(Guid id, CoachEditUserDto editedUser)
        {
            _logger.LogTrace("CoachEditUser/id");
            return await _logic.CoachEditUser(id, editedUser);
        }

        [HttpDelete("delete/{id}")]
        public async Task<User> DeleteUser(Guid id)
        {
            _logger.LogInformation("User deleted.");
            return await _logic.DeleteUser(id);
        }
     }
}
