using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Common.BusinessLayer.CommandStack.EventHandlers;
using SFI.Microservice.Common.BusinessLayer.CommandStack.QueryHandlers;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Common.DatabaseLayer;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Events.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Events.BusinessLayer.CommandStack.EventHandlers;
using SFI.Microservice.Events.BusinessLayer.Services;
using SFI.Microservice.Events.DatabaseLayer.Repositories;
using SimpleInjector;

namespace SFI.Microservice.Events.Configuration
{
    public static class SimpleInjectorConfiguration
    {
        public static Container ConfigureSimpleInjector(Container container, IConfiguration configuration)
        {
            container.Register(typeof(IReadService<>), typeof(ReadService<>));

            container.Register<IWriteService<EventItem>, WriteService<EventItem>>();

            container.Register<IRepository<EventItem>, MemoryRepository<EventItem>>();

            container.RegisterDecorator<IReadService<EventItem>, MemoryCacheService<EventItem>>();

            container.Collection.Register(typeof(IEventHandler<>), typeof(EventCreatedEventHandler).Assembly);

            container.RegisterInitializer<IRepository<EventItem>>(repo =>
            {
                repo.Create(new EventItem
                {
                    Id = 1,
                    Name = "SFI 2021"
                });
                repo.Create(new EventItem
                {
                    Id = 5,
                    Name = "C# Microservices"
                });
            });

            return container;
        }
    }
}
