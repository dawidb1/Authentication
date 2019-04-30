using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autentication.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {

        }

    }
}
