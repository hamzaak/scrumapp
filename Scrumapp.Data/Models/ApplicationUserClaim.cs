using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Scrumapp.Data.Models
{
    public class ApplicationUserClaim:IdentityUserClaim<string>
    {
    }
}
