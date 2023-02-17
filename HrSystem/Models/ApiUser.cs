using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HrSystem.Models
{
    public class ApiUser : IdentityUser
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
