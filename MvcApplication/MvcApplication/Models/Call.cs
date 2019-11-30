using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MvcApplication.Models
{
    public partial class Call
    {
        [DisplayName("Call index")]
        public int CallId { get; set; }
        public int? MasterId { get; set; }
        public int? SectionId { get; set; }
        public int? LanternId { get; set; }

        [DisplayName("Date call")]
        public DateTime? DateCall { get; set; }

        public virtual Lantern Lantern { get; set; }
        public virtual Master Master { get; set; }
        public virtual Section Section { get; set; }
    }
}
