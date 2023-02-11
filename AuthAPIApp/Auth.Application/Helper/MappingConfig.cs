using Auth.Application.Dto;
using Auth.DataAcces.Persistence.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Helper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<User, UserDto>().ReverseMap();
                config.CreateMap<User, LoginDto>().ReverseMap(); 
                config.CreateMap<User, RegisterDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
