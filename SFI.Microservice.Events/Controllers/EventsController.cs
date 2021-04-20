using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly ICommandHandler<CreateEventCommand, EventDto> _createEventCommandHandler;
        private readonly IReadService<EventDescription> _eventDescriptionReadService;

        /// <inheritdoc />
        public EventsController(IReadService<EventItem> eventItemReadService, ICommandHandler<CreateEventCommand, EventDto> createEventCommandHandler)
        {
            _eventItemReadService = eventItemReadService;
            _createEventCommandHandler = createEventCommandHandler;
        }

        [HttpGet]
        public List<EventDto> GetAllItems()
        {
            return _eventItemReadService.GetAll<EventDto>();
        }

        /// <summary>
        /// Gets single event item
        /// </summary>
        /// <param name="eventId">Id of event to retrieve</param>
        /// <returns>Class containing event data</returns>
        [HttpGet("{eventId:int}")]
        public EventDto GetSingleItem(int eventId)
        {
            return _eventItemReadService.GetById<EventDto>(eventId, CancellationToken.None);
        }

        [HttpPost]
        public async Task<EventDto> CreateNewItem(CreateEventDto eventData)
        {
            return await _createEventCommandHandler.ExecuteAsync(new CreateEventCommand
            {
                Name = eventData.Name
            }, CancellationToken.None);
        }

        [HttpGet("descriptions")]
        public List<EventDescription> GetAllEventDescriptions()
        {
            return _eventDescriptionReadService.GetAll();
        }
    }
}
