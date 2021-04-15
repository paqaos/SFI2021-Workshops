using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Users.BusinessLayer.CommandStack.Commands;
using SFI.Microservice.Users.Dto;

namespace SFI.Microservice.Users.BusinessLayer.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<AddUserCommand, User>();
        }
    }
}
