using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Data.Models;

namespace TodoAPI.Repositories
{
	public class TodoCategoryRepo : ITodoCategoryRepo
	{
		private readonly TodoContext _context;
		public TodoCategoryRepo(TodoContext context)
		{
			_context = context;
		}

		public async Task AddTodoCategoryAsync(TodoCategory todoCategory)
		{
            await _context.TodoCategory.AddAsync(todoCategory);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<TodoCategory>> GetTodoCategoriesAsync()
		{

			return await _context.TodoCategory.Include(td=>td.Todos).ToListAsync();
		}
	}
}
