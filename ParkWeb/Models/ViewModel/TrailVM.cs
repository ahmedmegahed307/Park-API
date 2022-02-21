using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Models.ViewModel
{
    public class TrailVM
    {
        public IEnumerable<SelectListItem> ParkList { get; set; }
        public Trail trail { get; set; }
    }
}
