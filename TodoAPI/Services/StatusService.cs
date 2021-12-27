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
    public class StatusService : IStatusService
    {

        private readonly IMapper _mapper;
        private readonly IStatusRepo _repo;

        public StatusService(IMapper mapper, IStatusRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task AddStatusAsync(StatusCreateDTO statusCreateDTO)
        {
            var status = _mapper.Map<Status>(statusCreateDTO);
            await _repo.AddStatusAsync(status);

        }

        public async Task<StatusReadDTO> GetStatusByIdAsync(int id)
        {
            var result = await _repo.GetStatusByIdAsync(id);
            return _mapper.Map<StatusReadDTO>(result);
        }

        public async Task<IEnumerable<StatusReadDTO>> GetStatusesAsync()
        {
            var result = await _repo.GetStatusesAsync();
            return _mapper.Map<IEnumerable<StatusReadDTO>>(result);
        }

        public async Task<bool> UpdateStatusByIdAsync(int id, StatusCreateDTO statusCreateDTO)
        {
            var status = _mapper.Map<Status>(statusCreateDTO);
            return await _repo.UpdateStatusByIdAsync(id, status);
        }
    }
}
