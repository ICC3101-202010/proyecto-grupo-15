using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class SendingtextMultipleFiltersEventArgs:EventArgs
    {
        public string TexttoMultipleFilters { get; set; }
    }
}
