using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HueCitApp.DTOs
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
    }

    public class UserDtoExt
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public int? Role { get; set; }
    }
}
