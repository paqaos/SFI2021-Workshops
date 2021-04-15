using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SFI.Microservice.Common.BusinessLayer;
using SFI.Microservice.Common.BusinessLayer.CommandStack.QueryHandlers;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Users.BusinessLayer.CommandStack.Queries;
using SFI.Microservice.Users.Dto;

namespace SFI.Microservice.Users.BusinessLayer.CommandStack.QueryHandlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IReadService<User> _userReadService;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IReadService<User> userReadService, IMapper mapper)
        {
            _userReadService = userReadService;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<UserDto>> ExecuteQueryAsync(GetAllUsersQuery queryFilters, CancellationToken ct)
        {
            var users = _userReadService.GetAll();

            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
