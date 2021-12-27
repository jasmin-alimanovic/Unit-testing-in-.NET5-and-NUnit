using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models.DTOs;

namespace TodoAPI.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoReadDTO>> GetTodosAsync(string sort, string query, int? pageNumber);

        Task<int> GetTodosSize();

        Task<TodoReadDTO> AddTodoAsync(TodoCreateDTO todoCreateDTO);

        Task<TodoReadDTO> GetTodoByIdAsync(int id);

        Task<bool> UpdateTodoByIdAsync(int id, TodoUpdateDTO todo);
    }
}
