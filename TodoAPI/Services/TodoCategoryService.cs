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
    public class TodoCategoryService : ITodoCategoryService
    {
        private readonly ITodoCategoryRepo _repo;
        private readonly IMapper _mapper;
        public TodoCategoryService(ITodoCategoryRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        public async Task AddTodoCategoryAsync(TodoCategoryCreateDTO todoCategoryCreateDTO)
        {
            var todoCategory = _mapper.Map<TodoCategory>(todoCategoryCreateDTO);
            await _repo.AddTodoCategoryAsync(todoCategory);
        }

        //get todo category 

        public async Task<IEnumerable<TodoCategoryReadDTO>> GetTodoCategoriesAsync()
        {
           var result =  await _repo.GetTodoCategoriesAsync();
            return _mapper.Map<IEnumerable<TodoCategoryReadDTO>>(result);
        }
    }
}
