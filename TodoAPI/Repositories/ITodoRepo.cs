using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models.DTOs;
using TodoAPI.Data.Models;
namespace TodoAPI.Repositories
{
    public interface ITodoRepo
    {
        Task<IEnumerable<Todo>> GetTodosAsync(string sort, string query, int? pageNumber);

        Task<Todo> AddTodoAsync(Todo todo);

        Task<Todo> GetTodoByIdAsync(int id);

        Task<int> GetTodosSize();

        Task<bool> UpdateTodoByIdAsync(int id, Todo todo);
    }
}
