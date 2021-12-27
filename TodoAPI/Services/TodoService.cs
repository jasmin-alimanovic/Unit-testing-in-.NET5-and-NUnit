using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models;
using TodoAPI.Data.Models.DTOs;
using TodoAPI.Repositories;

namespace TodoAPI.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepo _repo;
        private readonly IMapper _mapper;

        //constructor
        public TodoService(ITodoRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        //add todo

        public async Task<TodoReadDTO> AddTodoAsync(TodoCreateDTO todoCreateDTO)
        {
            var todo =  _mapper.Map<Todo>(todoCreateDTO);
            var savedTodo = await _repo.AddTodoAsync(todo);

            return _mapper.Map<TodoReadDTO>(savedTodo);

            
        }

        public async Task<TodoReadDTO> GetTodoByIdAsync(int id)
        {
            var result = await _repo.GetTodoByIdAsync(id);

            return _mapper.Map<TodoReadDTO>(result);
        }

        //get todo

        public async Task<IEnumerable<TodoReadDTO>> GetTodosAsync(string sort, string query, int? pageNumber)
        {
            var result =  await _repo.GetTodosAsync(sort, query, pageNumber);
            return _mapper.Map<IEnumerable<TodoReadDTO>>(result);
        }

        /// <summary>
        /// return the total number of todos
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetTodosSize()
        {
            return await _repo.GetTodosSize();
        }

        public async Task<bool> UpdateTodoByIdAsync(int id, TodoUpdateDTO todo)
        {
            var result = _mapper.Map<Todo>(todo);

            return await _repo.UpdateTodoByIdAsync(id, result);
        }
    }
}
