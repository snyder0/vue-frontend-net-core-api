using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarterApi.Features.Users;

namespace StarterApi.Common.Responses
{
    public static class ErrorMessages
    {
        public static class User
        {
            public const string EmailOrPasswordIsIncorrect = "'Email' or 'Password' is incorrect.";

            public const string EmailAlreadyExists = "An 'Email' with this name already exists.";
        }
    }
}
