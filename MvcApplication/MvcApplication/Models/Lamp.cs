using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MvcApplication.Models
{
    public partial class Lamp
    {
        public Lamp()
        {
            Lanterns = new HashSet<Lantern>();
        }

        [DisplayName("Lamp's id")]
        public int LampId { get; set; }

        [DisplayName("Lamp's name")]
        public string LampName { get; set; }

        [DisplayName("Lamp's power")]
        public int? LampPower { get; set; }

        [DisplayName("Lamp's service life")]
        public int? LampLife { get; set; }

        public virtual ICollection<Lantern> Lanterns { get; set; }
    }
}
