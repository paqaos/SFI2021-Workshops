﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.DatabaseModel;

namespace SFI.Microservice.Users.BusinessLayer.Services
{
    public interface IUserService
    {
        User UpdatePublicInfo(User user, string nickname, string photoUrl);
    }
}
