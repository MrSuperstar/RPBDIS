using System;
using System.Collections.Generic;

namespace MvcApplication.Models
{
    public partial class SectionsLighting
    {
        public int SectionsLightingId { get; set; }
        public int? SectionId { get; set; }
        public int? LanternId { get; set; }
        public int? WorkTime { get; set; }

        public virtual Lantern Lantern { get; set; }
        public virtual Section Section { get; set; }
    }
}
