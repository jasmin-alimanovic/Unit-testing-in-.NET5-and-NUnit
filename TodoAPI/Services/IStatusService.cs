using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models;
using TodoAPI.Data.Models.DTOs;

namespace TodoAPI.Services
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusReadDTO>> GetStatusesAsync();

        Task AddStatusAsync(StatusCreateDTO statusCreateDTO);

        Task<StatusReadDTO> GetStatusByIdAsync(int id);

        Task<bool> UpdateStatusByIdAsync(int id, StatusCreateDTO statusCreateDTO);
    }
}
