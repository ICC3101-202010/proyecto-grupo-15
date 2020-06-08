using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class SendingvideocaracteristicsEventArgs:EventArgs
    {
        public string Videoname { get; set; }
        public string Genero { get; set; }
        public string Categoria { get; set; }
        public string Actor { get; set; }
        public string Director { get; set; }
        public string Estudio { get; set; }
        public string Descripcion { get; set; }
        public string Sexo { get; set; }
        public string Edad { get; set; }
        public string Resolution { get; set; }
        public string path { get; set; }
        public string duracion { get; set; }
        public string byts { get; set; }




    }
}
