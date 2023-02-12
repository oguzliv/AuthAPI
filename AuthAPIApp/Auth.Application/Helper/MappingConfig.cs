using Auth.Application.Dto;
using Auth.Application.Dto.Request;
using Auth.Application.Dto.Response;
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
                config.CreateMap<User, RegisterDto>().ReverseMap();
                config.CreateMap<User, LoginModel>().ReverseMap(); 
                config.CreateMap<User, RegisterModel>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
