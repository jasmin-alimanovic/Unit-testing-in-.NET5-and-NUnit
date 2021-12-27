using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models;

namespace TodoAPI.Repositories
{
    public interface ITodoCategoryRepo
    {
        Task<IEnumerable<TodoCategory>> GetTodoCategoriesAsync();

        Task AddTodoCategoryAsync(TodoCategory todo);
    }
}
