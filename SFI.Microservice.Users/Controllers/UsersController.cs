using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Common.BusinessLayer.CommandStack.QueryHandlers;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Users.BusinessLayer.CommandStack.Commands;
using SFI.Microservice.Users.BusinessLayer.CommandStack.Queries;
using SFI.Microservice.Users.Dto;
using SFI.Microservice.Users.Dto.Requests;

namespace SFI.Microservice.Users.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IQueryHandler<GetAllUsersQuery, List<UserDto>> _getAllUsersQueryHandler;
        private readonly ICommandHandler<AddUserCommand, UserDto> _addUserCommandHandler;
        private readonly IReadService<User> _userReadService;
        private readonly IWriteService<User> _userWriteService;

        public UsersController(IQueryHandler<GetAllUsersQuery, List<UserDto>> getAllUsersQueryHandler, IReadService<User> userReadService, ICommandHandler<AddUserCommand, UserDto> addUserCommandHandler, IWriteService<User> userWriteService)
        {
            _getAllUsersQueryHandler = getAllUsersQueryHandler;
            _userReadService = userReadService;
            _addUserCommandHandler = addUserCommandHandler;
            _userWriteService = userWriteService;
        }

        /// <summary>
        /// Get all users 
        /// </summary>
        /// <param name="ct">Cancellation token for processing of item</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserDto>> GetAllUsersAsync(CancellationToken ct)
        {
            var result = await _getAllUsersQueryHandler.ExecuteQueryAsync(new GetAllUsersQuery(), ct);

            return result;
        }

        [HttpGet("{userId:int}")]
        public UserDto GetUserData(int userId, CancellationToken ct)
        {
            return _userReadService.GetById<UserDto>(userId, ct);
        }

        [HttpPost("")]
        public async Task<UserDto> CreateUserAsync(CreateUserDto userData, CancellationToken ct)
        {
            return await _addUserCommandHandler.ExecuteAsync(new AddUserCommand
            {
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Nickname = userData.Nickname
            }, ct);
        }

        [HttpDelete("{userId:int}")]
        public Task<bool> DeleteUserAsync(int userId, CancellationToken ct)
        {
            var user = _userReadService.GetById(userId);

            return Task.FromResult(_userWriteService.Delete(user));
        }
    }
}
