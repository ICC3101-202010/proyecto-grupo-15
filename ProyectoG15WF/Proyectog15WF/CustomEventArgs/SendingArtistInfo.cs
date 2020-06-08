using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class SendingArtistInfo : EventArgs
    {
        public string ArtistText { get; set; }
        public string Usernametext { get; set; }
        public string AgeArtist { get; set; }
        public string GenderArtist { get; set; }

    }
}
