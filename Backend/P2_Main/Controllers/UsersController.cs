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


        // JOSH: create CreateUserDto DTO:
        // UserName, Password, FullName, Phone, Email, TeamID, RoleID
        //[HttpPost]
        //public async Task<ActionResult<User>> CreateUser(CreateUserDto createUser)
        //{
        //    return await _logic.CreateUser(createUser.UserName, createUser.Password, createUser.FullName, createUser.PhoneNumber, CreateUser.email, createUser.TeamID, createUser.RoleID);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            return await _logic.GetUserById(id);
        }

        [HttpGet("roles")]
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _logic.GetRoles();
        }

        [HttpGet("roles/{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            return await _logic.GetRoleById(id);
        }


        // Simple editing       --> probably want to pass in a User object with edited fields, then alter the one in the context -- always a valid user becuase they are logged in -- FullName, Email, Password, PhoneNumber can be changed
        // JOSH: create EditUserDto DTO:
        // FullName, Email, Password, Phone
        // May have to pass this in differently
        //[HttpPut("edit/{id}")]
        //public async Task<ActionResult<User>> EditUser(Guid id, UserEditDto editedUser)
        //{
        //    User editUser = new User()
        //    {
        //      UserID = id,
        //      FullName = editedUser.FullName,
        //      Email = editedUser.Email,
        //      Password = editedUser.Password,
        //      PhoneNumber = editedUser.Phone
        //     };
        //    return await _logic.EditUser(editUser);
        //}



        // Coach access required below      --> same as above -- provide drop-down menu or other list to select users to edit -- can edit any field but ID
        // JOSH: create CoachEditUserDto DTO:
        // UserName, FullName, Email, Password, Phone, TeamID, RoleID
        // May have to pass this in differently
        //[HttpPut("coach/edit/{id}")]
        //public async Task<ActionResult<User>> CoachEditUser(Guid id, CoachEditUserDto editedUser)
        //{
        //    User editUser = new User()
        //    {
        //        UserID = id,
        //        UserName = editedUser.UserName,
        //        FullName = editedUser.FullName,
        //        Email = editedUser.Email,
        //        Password = editedUser.Password,
        //        PhoneNumber = editedUser.Phone,
        //        TeamID = editedUser.TeamID,
        //        RoleID = editedUser.RoleID
        //    };
        //    return await _logic.CoachEditUser(editUser);
        //}

        [HttpDelete("delete/{id}")]
        public async Task DeleteUser(Guid id)
        {
            await _logic.DeleteUser(id);
            _logger.LogInformation("User deleted.");
        }
     }
}
