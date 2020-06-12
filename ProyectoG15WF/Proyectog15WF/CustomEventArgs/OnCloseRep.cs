using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class OnCloseRep:EventArgs
    {

        public string songName { get; set; }
        public string videoName { get; set; }
        public int MediaOnClose { get; set; }
        public string Username { get; set; }


    }
}
