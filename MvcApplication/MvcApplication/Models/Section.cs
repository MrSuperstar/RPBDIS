using System;
using System.Collections.Generic;

namespace MvcApplication.Models
{
    public partial class Section
    {
        public Section()
        {
            Calls = new HashSet<Call>();
            SectionsLightings = new HashSet<SectionsLighting>();
        }

        public int SectionId { get; set; }
        public int? StreetId { get; set; }
        public int? SectionNumber { get; set; }

        public virtual Street Street { get; set; }
        public virtual ICollection<Call> Calls { get; set; }
        public virtual ICollection<SectionsLighting> SectionsLightings { get; set; }
    }
}
