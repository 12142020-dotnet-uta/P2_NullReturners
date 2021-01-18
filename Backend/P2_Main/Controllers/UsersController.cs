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

        // what are we passing in here
        //[HttpPost]
        //public async Task<User> CreateUser()
        //{
        //    return await _logic.CreateUser();
        //}

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

        //[HttpPost("roles/{id}")]
        //public async Task<IEnumerable<Role>> GetRole(int id)
        //{
        //    return await _logic.GetRoleById(id);
        //}

        // Simple editing
        //[HttpPut("edit/{id}")]
        //public async Task<User> EditUser(Guid id)
        //{
        //    return await _logic.EditUser(id);
        //}

        

        // Coach access required below
        //[HttpPut("edit/{id}")]
        //public async Task<User> CoachEditUser(Guid id)
        //{
        //    return await _logic.CoachEditUser(id);
        //}

        //[HttpDelete("delete/{id}")]
        //public async Task<User> DeleteUser(Guid id)
        //{
        //    return await _logic.DeleteUser(id);
        //}




    }
}
