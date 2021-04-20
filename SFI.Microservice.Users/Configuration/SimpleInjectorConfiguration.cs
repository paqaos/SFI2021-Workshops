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
            container.Register(typeof(IWriteService<>), typeof(WriteService<>));
            container.Register(typeof(IReadService<>), typeof(ReadService<>));
            container.Register(typeof(IQueryHandler<,>), typeof(GetAllUsersQueryHandler).Assembly);
            container.Register(typeof(ICommandHandler<,>), typeof(GetAllUsersQueryHandler).Assembly);

            container.Register<IRepository<User>, EFUsersRepository>();

            return container;
        }
    }
}
