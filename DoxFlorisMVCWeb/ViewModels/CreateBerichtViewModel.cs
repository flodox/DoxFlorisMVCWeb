using DoxFlorisMVCWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoxFlorisMVCWeb.ViewModels
{
    public class CreateBerichtViewModel
    {
        public Bericht Bericht { get; set; }
        public SelectList Leden { get; set; }
    }
}
