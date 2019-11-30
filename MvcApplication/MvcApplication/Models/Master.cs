using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MvcApplication.Models
{
    public partial class Master
    {
        public Master()
        {
            Calls = new HashSet<Call>();
        }

        public int MasterId { get; set; }

        [DisplayName("Sensei name")]
        public string MasterName { get; set; }

        public virtual ICollection<Call> Calls { get; set; }
    }
}
