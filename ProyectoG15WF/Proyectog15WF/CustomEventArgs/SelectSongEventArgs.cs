using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class SelectSongEventArgs : EventArgs
    {
        public string Selectedsong { get; set; }
        public string path{ get; set; }

    }
}
