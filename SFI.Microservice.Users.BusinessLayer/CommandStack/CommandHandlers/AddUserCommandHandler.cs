using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Users.BusinessLayer.CommandStack.Commands;
using SFI.Microservice.Users.Dto;

namespace SFI.Microservice.Users.BusinessLayer.CommandStack.CommandHandlers
{
    public class AddUserCommandHandler : ICommandHandler<AddUserCommand, UserDto>
    {
        private readonly IWriteService<User> _userWriteService;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IWriteService<User> userWriteService, IMapper mapper)
        {
            _userWriteService = userWriteService;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<UserDto> ExecuteAsync(AddUserCommand command, CancellationToken ct)
        {
            var newUser = _mapper.Map<User>(command);
            var newUserData = _userWriteService.Create(newUser);

            return _mapper.Map<UserDto>(newUserData);
        }
    }
}
