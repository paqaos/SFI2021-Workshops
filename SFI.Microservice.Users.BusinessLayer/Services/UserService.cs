using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.DatabaseModel;

namespace SFI.Microservice.Users.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        /// <inheritdoc />
        public User UpdatePublicInfo(User user, string nickname, string photoUrl)
        {
            user.Nickname = nickname;
            user.Photo = photoUrl;

            return user;
        }
    }
}
