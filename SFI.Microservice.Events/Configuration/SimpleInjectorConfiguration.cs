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
            
            return container;
        }
    }
}
