using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApp_WebApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        //[Required]
        [PersonalData]
        public string Address { get; set;}

        public bool IsModerator { get; set; }

    }
}
