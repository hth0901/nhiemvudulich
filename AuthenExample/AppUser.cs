using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenExample
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}
