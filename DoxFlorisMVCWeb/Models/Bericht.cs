using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoxFlorisMVCWeb.Models
{
    public class Bericht
    {
        public int Id { get; set; }
        public string titel { get; set; }
        public string inhoud { get; set; }
        [DataType(DataType.Date)]
        public DateTime datum { get; set; }
        public int lidId { get; set; }
        public Lid lid { get; set; }
    }
}
