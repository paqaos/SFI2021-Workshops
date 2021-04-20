using SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Common.DatabaseLayer;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Participants.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Participants.DatabaseLayer.Repositories;
using SimpleInjector;

namespace SFI.Microservice.Participants.Configuration
{
    public class SimpleInjectorConfiguration
    {
        public static Container ConfigureSimpleInjector(Container container)
        {
            container.Register(typeof(IWriteService<>), typeof(WriteService<>));
            container.Register(typeof(IReadService<>), typeof(ReadService<>));

            container.Register<IRepository<Participant>, DapperParticipationsRepository>();

            container.Register(typeof(ICommandHandler<,>), typeof(MarkAsParticipantCommandHandler).Assembly);


            return container;
        }
    }
}
