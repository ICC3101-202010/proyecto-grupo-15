using Controllers;
using Proyectog15WF.Contollers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proyectog15WF
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppForm appForm = new AppForm();
            ArtistController artistController = new ArtistController(appForm);
            UserController userController = new UserController(appForm);
            Songcontroller songcontroller = new Songcontroller(appForm);
            VideoController videoController = new VideoController(appForm);
            Application.Run(appForm);
        }
    }
}
