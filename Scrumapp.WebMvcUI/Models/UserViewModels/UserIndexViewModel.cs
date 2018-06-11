using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrumapp.WebMvcUI.Models.UserViewModels
{
    public class UserIndexViewModel
    {
        public IEnumerable<UserDetailViewModel> Users { get; set; }
    }
}
