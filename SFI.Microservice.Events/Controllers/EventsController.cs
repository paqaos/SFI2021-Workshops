using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Events.BusinessLayer.CommandStack.Commands;
using SFI.Microservice.Events.Dto;

namespace SFI.Microservice.Events.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IReadService<EventItem> _eventItemReadService;

        /// <inheritdoc />
        public EventsController(IReadService<EventItem> eventItemReadService)
        {
            _eventItemReadService = eventItemReadService;
        }

        [HttpGet]
        public List<EventItem> GetAllItems()
        {
            return _eventItemReadService.GetAll();
        }
    }
}
