using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoxFlorisMVCWeb.Models
{
    public class Lid
    {
        public int Id { get; set; }
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        [DataType(DataType.Date)]
        public DateTime geboortedatum { get; set; }
        public string straat { get; set; }
        public string huisnummer { get; set; }
        public string postcode { get; set; }
        public string land { get; set; }
        public string email { get; set; }
        public string telefoon { get; set; }
        public ICollection<Speedrun> speedruns { get; set; }
        public ICollection<Bericht> berichten { get; set; }
    }
}
