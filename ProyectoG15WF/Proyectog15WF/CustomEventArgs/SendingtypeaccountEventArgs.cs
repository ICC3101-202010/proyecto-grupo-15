using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class SendingtypeaccountEventArgs : EventArgs
    {
        public string Usernametext { get; set; }
        public string Agetext { get; set; }
        public string Typeaccount { get; set; }
        public string Privacidad { get; set; }
        public string Genero { get; set; }

    }
}
