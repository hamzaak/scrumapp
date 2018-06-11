using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Scrumapp.WebMvcUI.Helpers
{
    public class SelectHelper
    {
        public static SelectList GetBooleanSelect(bool selectedValue)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem{Value = "false", Text = "No"},
                new SelectListItem{Value = "true", Text = "Yes"}
            };

            return new SelectList(items, "Value", "Text", selectedValue ? "true" : "false");
        }
        
    }
}
