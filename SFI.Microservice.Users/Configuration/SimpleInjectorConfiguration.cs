using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer;
using SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Common.BusinessLayer.CommandStack.QueryHandlers;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Common.DatabaseLayer;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Users.BusinessLayer.CommandStack.QueryHandlers;
using SFI.Microservice.Users.DatabaseLayer.Repositories;
using SimpleInjector;

namespace SFI.Microservice.Users.Configuration
{
    public static class SimpleInjectorConfiguration
    {
        public static Container ConfigureSimpleInjector(Container container)
        {
          
            return container;
        }
    }
}
