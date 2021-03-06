﻿using System;
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
        public async Task<ActionResult<EquipmentRequest>> GetEquipmentRequest(int id)
        {
            return await _logic.GetEquipmentRequestById(id);
        }
        [HttpPost]
        public async Task<ActionResult<EquipmentRequest>> CreateEquipmentRequest(CreateEquipmentRequestDto equipmentRequest)
        {
            return await _logic.CreateEquipmentRequest(equipmentRequest);
        }
        [HttpPut("edit/{id}")]
        public async Task<ActionResult<EquipmentRequest>> EditEquipmentRequest(int id, EditEquipmentRequestDto equipmentRequest)
        {
            return await _logic.EditEquipmentRequest(id, equipmentRequest);
        }
        [HttpGet("items")]
        public async Task<IEnumerable<EquipmentItem>> GetEquipmentItems()
        {
            return await _logic.GetEquipmentItems();
        }
        [HttpGet("items/{id}")]
        public async Task<ActionResult<EquipmentItem>> GetEquipmentItemById(int id)
        {
            return await _logic.GetEquipmentItemtById(id);
        }
    }
}
