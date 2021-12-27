using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data.Models;
using TodoAPI.Data.Models.DTOs;

namespace TodoAPI.Mapping_Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<StatusCreateDTO, Status>();
            CreateMap<Status, StatusReadDTO>();
        }
    }
}
