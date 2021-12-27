using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models;
using TodoAPI.Data.Models.DTOs;
using TodoAPI.Services;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _service;

        public StatusController(IStatusService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetStatusesAsync()
        {
            var statuses = await _service.GetStatusesAsync();
            return Ok(statuses);
        }
        [HttpGet("s")]
        public IActionResult GetStatussAsync()
        {
            var statuses = new{
                id=1,
                name="aktivan"
            };
            return Ok(statuses);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStatusByIdAsync(int id)
        {
            var statuses = await _service.GetStatusByIdAsync(id);
            Console.WriteLine(id);
            return Ok(statuses);
        }

        [HttpPost]
        public async Task<IActionResult> AddStatusAsync(StatusCreateDTO statusCreateDTO)
        {
            await _service.AddStatusAsync(statusCreateDTO);
            return StatusCode(201);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStatusByIdAsync(int id, [FromBody] StatusCreateDTO status)
        {
            await _service.UpdateStatusByIdAsync(id, status);
            return StatusCode(201);
        }
    }
}
