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
    public class EquipmentController : ControllerBase
    {
        private readonly LogicClass _logic;
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(LogicClass logic, ILogger<EquipmentController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentRequests()
        {
            return await _logic.GetEquipmentRequests();
        }

        [HttpGet("{id}")]
        public async Task<EquipmentRequest> GetEquipmentRequest(int id)
        {
            return await _logic.GetEquipmentRequestById(id);
        }

        //[HttpPost]
        //public async Task<EquipmentRequest> CreateEquipmentRequest()
        //{
        //    return await _logic.CreateEquipmentRequest();
        //}

        //[HttpPut("/edit/{id}")]
        //public async Task<EquipmentRequest> EditEquipmentRequest(int id)
        //{
        //    return await _logic.EditEquipmentRequest(id);
        //}

    }
}
