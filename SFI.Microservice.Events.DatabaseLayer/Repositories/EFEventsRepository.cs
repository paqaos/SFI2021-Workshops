using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFI.Microservice.Common.DatabaseLayer;
using SFI.Microservice.Common.DatabaseModel;

namespace SFI.Microservice.Events.DatabaseLayer.Repositories
{
    public class EFEventsRepository : IRepository<EventItem>
    {
        private EventsDbContext _dbContext;

        public EFEventsRepository(EventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public List<EventItem> ReadAll()
        {
            var items = _dbContext.Events.Where(x => !x.IsDeleted);
          
            return items.ToList();
        }

        /// <inheritdoc />
        public EventItem Read(long id)
        {
            return _dbContext.Events.SingleOrDefault(x => x.Id == id);
        }

        /// <inheritdoc />
        public EventItem Create(EventItem input)
        {
            var item = _dbContext.Events.Add(input);
            _dbContext.SaveChanges();

            return item.Entity;
        }

        /// <inheritdoc />
        public EventItem Update(EventItem input)
        {
            var item = _dbContext.Events.Update(input);
            _dbContext.SaveChanges();

            return item.Entity;
        }
    }
}
