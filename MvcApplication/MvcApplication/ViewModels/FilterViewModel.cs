using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApplication.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(string name)
        {
            SelectedName = name;
        }

        public IEnumerable<Models.Lamp> Lamps { get; private set; }
        public IEnumerable<Models.Master> Masters { get; private set; }
        public IEnumerable<Models.Lantern> Lanterns { get; private set; }
        public string SelectedName { get; private set; }
    }
}
