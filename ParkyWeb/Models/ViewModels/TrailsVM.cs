using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models.ViewModels
{
    public class TrailsVM
    {
        public IEnumerable<SelectListItem> NationalParksList { get; set; }
        public Trail Trail { get; set; }
    }
}