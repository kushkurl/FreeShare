﻿using Microsoft.AspNetCore.Identity;

namespace FreeShare.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

}