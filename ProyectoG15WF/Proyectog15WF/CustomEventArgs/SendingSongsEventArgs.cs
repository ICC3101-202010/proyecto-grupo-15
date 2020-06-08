using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class SendingSongsEventArgs:EventArgs
    {
        public string Sendinguser { get; set; }
    }
}
