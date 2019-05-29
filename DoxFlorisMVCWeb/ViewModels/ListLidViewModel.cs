using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoxFlorisMVCWeb.ViewModels
{
    public class ListLidViewModel
    {
        public string NaamSearch { get; set; }
        public List<DoxFlorisMVCWeb.Models.Lid> Leden { get; set; }
    }
}
