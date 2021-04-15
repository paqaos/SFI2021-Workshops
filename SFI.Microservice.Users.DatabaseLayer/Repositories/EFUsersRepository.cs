using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.DatabaseLayer;
using SFI.Microservice.Common.DatabaseModel;

namespace SFI.Microservice.Users.DatabaseLayer.Repositories
{
    public class EFUsersRepository : IRepository<User>
    {
        private UsersDbContext _dbContext;

        public EFUsersRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public List<User> ReadAll()
        {
            return _dbContext.Users.Where(x => !x.IsDeleted).ToList();
        }

        /// <inheritdoc />
        public User Read(long id)
        {
            return _dbContext.Users.SingleOrDefault(x => x.Id == id);
        }

        /// <inheritdoc />
        public User Create(User input)
        {
            var item = _dbContext.Users.Add(input);
            _dbContext.SaveChanges();

            return item.Entity;
        }

        /// <inheritdoc />
        public User Update(User input)
        {
            var item = _dbContext.Users.Update(input);
            _dbContext.SaveChanges();

            return item.Entity;
        }
    }
}
