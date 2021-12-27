using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Data.Models;

namespace TodoAPI.Repositories
{
    public class StatusRepo : IStatusRepo
    {
        private readonly TodoContext _context;
        
        
        public StatusRepo(TodoContext context)
        {
            _context = context;
        }


        public async Task AddStatusAsync(Status status)
        {
            await _context.AddAsync(status);
            await _context.SaveChangesAsync();
        }

        public async Task<Status> GetStatusByIdAsync(int id)
        {
            return await _context.Statuses.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Status>> GetStatusesAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<bool> UpdateStatusByIdAsync(int id, Status status)
        {
            var statusDb = await GetStatusByIdAsync(id);
            statusDb.Name = status.Name;
            return await _context.SaveChangesAsync() >=0;
        }
    }
}
