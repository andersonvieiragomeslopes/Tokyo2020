using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokyo2020.Domain.Models
{
    public class Country
    {
        [Key]
        public int IdCountry { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [JsonIgnore]
        public virtual ICollection<Athlete> Athletes { get; set; }
    }
}
