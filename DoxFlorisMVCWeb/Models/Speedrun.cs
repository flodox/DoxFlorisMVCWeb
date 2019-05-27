using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoxFlorisMVCWeb.Models
{
    public class Speedrun
    {
        public int Id { get; set; }
        public double snelheid { get; set; }
        [DataType(DataType.Date)]
        public DateTime datum { get; set; }
        public string plank { get; set; }
        public string zeil { get; set; }
        public int lidId { get; set; }
        public Lid lid { get; set; }
        


    }
}
