using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEventArgs
{
    public class RegisterEventArgs : EventArgs
    {
        public string Nametext { get; set; }
        public string Lastnametext { get; set; }
        public string Usernametext { get; set; }
        public string Email { get; set; }
        public string Passwordtext { get; set; }
    }
}
