using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Scrumapp.Data.Models
{
    public partial class ApplicationUserRole:IdentityUserRole<string>
    {
        public virtual string ProjectId { get; set; }
    }
}
