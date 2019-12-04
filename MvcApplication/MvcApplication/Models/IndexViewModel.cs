using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcApplication.ViewModels;

namespace MvcApplication.Models
{

    public class IndexViewModel
    {
        public IEnumerable<Lamp> Lamps { get; set; }
        public IEnumerable<Master> Masters { get; set; }
        public IEnumerable<Lantern> Lanterns { get; set; }

        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }

        public string LampName { get; set; }
        public string LanternName { get; set; }
        public string MasterName { get; set; }

        
    }
}
