using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models;
using TodoAPI.Data.Models.DTOs;

namespace TodoAPI.Mapping_Profiles
{
    public class TodoCategoryProfile :Profile
    {
        public TodoCategoryProfile()
        {
            CreateMap<TodoCategoryCreateDTO, TodoCategory>();
            CreateMap<TodoCategory, TodoCategoryReadDTO>();
        }
    }
}
