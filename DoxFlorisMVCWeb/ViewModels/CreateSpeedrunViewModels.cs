using DoxFlorisMVCWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoxFlorisMVCWeb.ViewModels
{
    public class CreateSpeedrunViewModels
    {
        public Speedrun Speedrun { get; set; }

        public SelectList Leden { get; set; }
    }
}
