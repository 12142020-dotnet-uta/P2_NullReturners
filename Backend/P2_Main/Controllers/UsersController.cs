﻿using System;
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
        private readonly Mapper _mapper;
        private readonly ILogger<UsersController> _logger;
        public UsersController(LogicClass logic, Mapper mapper, ILogger<UsersController> logger)
        {
            _logic = logic;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _logic.GetUsers();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            return _mapper.ConvertUserToUserDto(await _logic.GetUserById(id));
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
        [HttpPut("edit/{id}")]
        public async Task<ActionResult<User>> EditUser(Guid id, EditUserDto editedUser)
        {
            return await _logic.EditUser(id, editedUser);
        }
        [HttpPut("coach/edit/{id}")]
        public async Task<ActionResult<User>> CoachEditUser(Guid id, CoachEditUserDto editedUser)
        {
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
