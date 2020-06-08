using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class SelectVideoEventArgs : EventArgs
    {
        public string Selectedvideo { get; set; }
        public string Path { get; set; }

    }        
}

