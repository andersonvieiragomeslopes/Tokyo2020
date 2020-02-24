using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokyo2020.Domain.Models
{
    public class Athlete
    {
        [Key]
        public int IdAthlete { get; set; }
        public int IdCountry { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        [JsonIgnore]
        public virtual Country Countries { get; set; }
    }
}
