using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SFI.Microservice.Common.BusinessLayer;
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
        /// <summary>
        /// Get all users 
        /// </summary>
        /// <param name="ct">Cancellation token for processing of item</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserDto>> GetAllUsersAsync(CancellationToken ct)
        {
            
            return new List<UserDto>();
        }
    }
}
