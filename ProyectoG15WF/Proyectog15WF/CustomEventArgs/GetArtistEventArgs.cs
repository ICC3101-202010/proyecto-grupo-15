using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class GetArtistEventArgs:EventArgs
    {
        public string ArtistName { get; set; }
    }
}
