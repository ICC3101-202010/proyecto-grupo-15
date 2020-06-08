using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class SendingsongcaracteristicsEventArgs:EventArgs
    {
        public string Nombrecancion { get; set; }
        public string Compositor { get; set; }
        public string Genero { get; set; }
        public string Discografia { get; set; }
        public string Estudio { get; set; }
        public string Letra { get; set; }
        public string Categoria { get; set; }
        public string Sexo { get; set; }
        public string Edad { get; set; }
        public string path { get; set; }
        public string duracion { get; set; }
        public string byts { get; set; }




    }
}
