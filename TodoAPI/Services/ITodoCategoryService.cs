using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models;
using TodoAPI.Data.Models.DTOs;

namespace TodoAPI.Services
{
    public interface ITodoCategoryService
    {
        Task<IEnumerable<TodoCategoryReadDTO>> GetTodoCategoriesAsync();

        Task AddTodoCategoryAsync(TodoCategoryCreateDTO todoCategoryCreateDTO);
    }
}
