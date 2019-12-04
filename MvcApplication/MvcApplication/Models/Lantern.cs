using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApplication.Models
{
    public partial class Lantern
    {
        public Lantern()
        {
            Calls = new HashSet<Call>();
            SectionsLightings = new HashSet<SectionsLighting>();
        }

        [Key]
        [Display(Name = "Id")]
        public int LanternId { get; set; }

        [ForeignKey("Lamp")]
        [Display(Name = "Lamp's name")]
        public int? LampId { get; set; }

        [Display(Name = "Lantern's name")]
        public string LanternName { get; set; }

        [Display(Name = "Lantern's type")]
        public string LanternType { get; set; }

        [Display(Name = "Lantern's servicebility")]
        public bool? IsOperable { get; set; }

        public virtual Lamp Lamp { get; set; }
        public virtual ICollection<Call> Calls { get; set; }
        public virtual ICollection<SectionsLighting> SectionsLightings { get; set; }
    }
}
