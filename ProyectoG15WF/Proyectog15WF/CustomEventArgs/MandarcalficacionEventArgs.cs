using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class MandarcalficacionEventArgs:EventArgs
    {
        public int Calification { get; set; }
        public string Namecancion { get; set; }
    }
}
