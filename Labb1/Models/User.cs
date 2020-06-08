using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.Models
{
   
    public class User:IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string StreetAddress { get; set; }
        [PersonalData]
        public int Postnumber { get; set; }
        [PersonalData]
        public string City { get; set; }
        [PersonalData]
        public string Country { get; set; }
    }
}
