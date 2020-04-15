using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace apiindserver.Models.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.User, User>();
            CreateMap<Models.Criteria, Criteria>();
        }
    }
}
