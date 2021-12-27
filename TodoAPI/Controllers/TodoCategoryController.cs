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
    public class TodoCategoryController : ControllerBase
    {
        private readonly ITodoCategoryService _service;
        public TodoCategoryController(ITodoCategoryService service)
        {
            _service = service;
        }
        

        [HttpGet]
        public async Task<IActionResult> GetTodoCategoriesAsync()
        {
            IEnumerable<TodoCategoryReadDTO> result = await _service.GetTodoCategoriesAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodoCategoryAsync([FromBody] TodoCategoryCreateDTO todoCategoryCreateDTO)
        {

            await _service.AddTodoCategoryAsync(todoCategoryCreateDTO);
            return StatusCode(201);
        }
    }
}
