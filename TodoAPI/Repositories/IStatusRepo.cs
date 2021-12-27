using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models;

namespace TodoAPI.Repositories
{
    public interface IStatusRepo
    {
        Task<IEnumerable<Status>> GetStatusesAsync();
        Task AddStatusAsync(Status status);
        Task<Status> GetStatusByIdAsync(int id);
        Task<bool> UpdateStatusByIdAsync(int id, Status status);
    }
}
