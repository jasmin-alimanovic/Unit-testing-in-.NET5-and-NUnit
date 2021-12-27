using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models;
using TodoAPI.Data.Models.DTOs;
using TodoAPI.Services;
using System.Numerics;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        /// <summary>
        /// function that handles get requests to /api/todo/
        /// </summary>
        /// <returns>list of objects of type TodoReadDTO</returns>
        [HttpGet]
        public async Task<IActionResult> GetTodosAsync(string sort, string query, int? pageNumber = 1)
        {
            pageNumber ??= 1;

            IEnumerable<TodoReadDTO> todos = await _service.GetTodosAsync(sort, query, pageNumber);
            if (todos == null)
            {
                return NotFound();
            }

            string route = "https://localhost:5001/api/Todo";
            int totalSize = await _service.GetTodosSize();

            string prevPage = null;
            if (pageNumber > 1)
                prevPage = $"{route}?pageNumber={pageNumber - 1}";
            

            string nextPage = null;
            if (pageNumber < (int)Math.Ceiling(totalSize / (double)4))
            {
                nextPage = $"{route}?pageNumber={pageNumber + 1}";
            }

            var result = new ReadWithOptionsDTO<TodoReadDTO>()
            {
                Count = totalSize,
                Results = todos,
                Previous =   prevPage,
                Next = nextPage
            };
            

            return Ok(result);
        }


       /// <summary>
       /// function that handles post request to /api/todo and adds new todo in database
       /// </summary>
       /// <param name="todoCreateDTO"></param>
       /// <returns>returns status code 201 if successful else returns ObjectResult response type Problem</returns>
        [HttpPost]
        public async Task<IActionResult> AddTodoAsync([FromBody] TodoCreateDTO todoCreateDTO)
        {
            try
            {
                if (todoCreateDTO == null)
                    throw new DbUpdateException();
                var savedTodo = await _service.AddTodoAsync(todoCreateDTO);
                
                return Created(nameof(AddTodoAsync), savedTodo);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"{ex.InnerException.Message}, Todo title: {todoCreateDTO.Title}");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);

            }
        }


        /// <summary>
        /// function that handles requests to /api/todo/[id]
        /// </summary>
        /// <param name="id"></param>
        /// <returns>singular object of type TodoReadDTO</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTodoByIdAsync(int id)
        {
            var result = await _service.GetTodoByIdAsync(id);
            if(result==null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTodoByIdAsync(int id, [FromBody] TodoUpdateDTO todo)
        {
            try
            {
                await _service.UpdateTodoByIdAsync(id, todo);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            
            return Ok();
        }
    }
}
