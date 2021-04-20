using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Events.BusinessLayer.CommandStack.Commands;
using SFI.Microservice.Events.Dto;
using SFI.Microservice.Events.Dto.Events;

namespace SFI.Microservice.Events.BusinessLayer.CommandStack.CommandHandlers
{
    public class CreateEventCommandHandler : ICommandHandler<CreateEventCommand, EventDto>
    {
        private readonly IWriteService<EventItem> _eventItemWriteService;
        private readonly IMapper _mapper;
        private readonly IAzureServiceBusHandler _azureServiceBusHandler;

        public CreateEventCommandHandler(IWriteService<EventItem> eventItemWriteService, IMapper mapper, IAzureServiceBusHandler azureServiceBusHandler)
        {
            _eventItemWriteService = eventItemWriteService;
            _mapper = mapper;
            _azureServiceBusHandler = azureServiceBusHandler;
        }

        /// <inheritdoc />
        public async Task<EventDto> ExecuteAsync(CreateEventCommand command, CancellationToken ct)
        {
            var item = _eventItemWriteService.Create(new EventItem
            {
                Name = command.Name,
                Date = DateTime.UtcNow
            });

            await _azureServiceBusHandler.SendEvent(new EventBaseCreatedEvent());

            return _mapper.Map<EventDto>(item);
        }
    }
}
