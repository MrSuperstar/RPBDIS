using System;
using System.Collections.Generic;

namespace MvcApplication.Models
{
    public partial class Street
    {
        public Street()
        {
            Sections = new HashSet<Section>();
        }

        public int StreetId { get; set; }
        public string StreetName { get; set; }
        public int? CountOfHouses { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
    }
}
