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
            return await _logic.GetUsers();
        }

        // what are we passing in here              returns newly created user or user found in db
        [HttpPost]
        public async Task<User> CreateUser(string userName, string password, string fullName, string phoneNumber, string email)
        {
            return await _logic.CreateUser(userName, password, fullName, phoneNumber, email);
        }

        [HttpGet("{id}")]
        public async Task<User> GetUser(Guid id)
        {
            return await _logic.GetUserById(id);
        }

        [HttpPost("roles")]
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _logic.GetRoles();
        }

        [HttpPost("roles/{id}")]
        public async Task<Role> GetRole(int id)
        {
            return await _logic.GetRoleById(id);
        }

        // Simple editing       --> probably want to pass in a User object with edited fields, then alter the one in the context -- always a valid user becuase they are logged in -- FullName, Email, Password, PhoneNumber can be changed
        [HttpPut("edit/{id}")]
        public async Task<User> EditUser(User editedUser)
        {
            return await _logic.EditUser(editedUser);
        }

        // Coach access required below      --> same as above -- provide drop-down menu or other list to select users to edit -- can edit any field but ID
        [HttpPut("edit/{id}")]
        public async Task<User> CoachEditUser(Guid id)
        {
            return await _logic.CoachEditUser(await _logic.GetUserById(id));
        }

        [HttpDelete("delete/{id}")]
        public async Task DeleteUser(Guid id)
        {
            await _logic.DeleteUser(id);
            _logger.LogInformation("User deleted.");
        }




    }
}
