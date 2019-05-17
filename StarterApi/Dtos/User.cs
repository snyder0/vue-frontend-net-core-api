using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterApi.Dtos
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class UserAuthenticationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class GetUserAuthenticationDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
