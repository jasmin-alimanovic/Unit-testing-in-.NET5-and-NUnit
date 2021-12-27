using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Data.Models;
using TodoAPI.Data.Models.DTOs;
using TodoAPI.Data.Models.Pagination;

namespace TodoAPI.Repositories
{
    public class TodoRepo : ITodoRepo
    {

        private readonly TodoContext _context;

        public TodoRepo(TodoContext context)
        {
            _context = context;
        }

        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return todo;
        }


        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            return await _context.Todos
                            .Include(t => t.TodoCategory)
                            .Include(t => t.Status)
                            .FirstOrDefaultAsync(t => t.Id == id);
            
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync(string sort, string query, int? pageNumber)
        {
            var todos =  await _context.Todos
                .Include(t => t.TodoCategory)
                .Include(t => t.Status)
                .OrderByDescending(t=>t.CreatedAt)
                .ToListAsync();

            //sort todos by title, date created, date finished
            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "title_desc":
                        todos = todos.OrderByDescending(t => t.Title).ToList();
                        break;
                    case "title":
                        todos = todos.OrderBy(t => t.Title).ToList();
                        break;
                    case "created_desc":
                        todos = todos.OrderByDescending(t => t.CreatedAt).ToList();
                        break;
                    case "created":
                        todos = todos.OrderBy(t => t.CreatedAt).ToList();
                        break;
                    case "finished_desc":
                        todos = todos.OrderByDescending(t => t.FinishedAt).ToList();
                        break;
                    case "finished":
                        todos = todos.OrderBy(t => t.FinishedAt).ToList();
                        break;
                    default:
                        break;
                }
            }

            //filter todos

            if(!string.IsNullOrWhiteSpace(query))
            {
                todos = todos.Where(t => 
                t.Title.Contains(query, StringComparison.CurrentCultureIgnoreCase)
                || t.Description.Contains(query, StringComparison.CurrentCultureIgnoreCase)
                ).ToList();
            }

            todos = PaginatedList<Todo>.Create(todos.AsQueryable(), pageNumber ?? 1, 4);

            return todos;
        }

        /// <summary>
        /// gets the total number of todoa
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetTodosSize()
        {
            return await _context.Todos.CountAsync();
        }

        public async Task<bool> UpdateTodoByIdAsync(int id, Todo todo)
        {
            var todoDb = await GetTodoByIdAsync(id);
            if(todo != null)
            {
                todoDb.Title = todo.Title;
                todoDb.StatusId = todo.StatusId;
                todoDb.TodoCategoryId = todo.TodoCategoryId;
                todoDb.IsDeleted = todo.IsDeleted;
                todoDb.FinishedAt = todo.FinishedAt;
                todoDb.Description = todo.Description;
            }
           

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
