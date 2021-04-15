using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer.CommandStack.Events;

namespace SFI.Microservice.Common.BusinessLayer.Services
{
    public interface IAzureServiceBusHandler
    {
        Task Initialize();

        Task SendEvent <T>(T eventData) where T : EventBase;
    }
}
