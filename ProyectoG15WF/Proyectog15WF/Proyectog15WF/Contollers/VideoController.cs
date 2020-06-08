using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Windows.Forms;
using CustomEventArgs;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Proyectog15WF.Contollers
{
    public class VideoController
    {
        List<Video> videos = new List<Video>();
        AppForm view;
        string curDir= Directory.GetCurrentDirectory();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        DateTime aDate = DateTime.Now;
        int total = 1;
        public VideoController(Form view)
        {
            ChargeVideos();
            this.view = view as AppForm;
            this.view.Searchingnamevideoorsong += OnSearchTextChanged;
            this.view.Reproducevideo += OnSelectedSongVideoEventArgs;
            this.view.Recivingvideo += OnverifyVideo;
            this.view.Videocaracteristics += OnVideocaracteristics;
            this.view.verifyVideoExist += OnverifyVideoExist;
            this.view.Totalitsofvideos += OnTotalitsofvideos;
            this.view.Reproduccionesname += Onvideoreproduction;
            this.view.Calificaciondelusuario += Onqualificationchanged;
            this.view.Recivingvideomultiplecriteria += OnBuscarvideo;
            DeserializeData();
        }

        public void SerializeData()
        {
            try
            {
                FileStream FS = new FileStream("Videos.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                binaryFormatter.Serialize(FS, videos);
                FS.Close();
            }

            catch
            {

            }
        }

        public void DeserializeData()
        {

            try
            {
                FileStream FS = new FileStream("Videos.bin", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                videos = (List<Video>)binaryFormatter.Deserialize(FS);
                FS.Close();
               

            }
            catch(Exception e)
            {
               

            }
        }

        public void OnSerialize(object sender, EventArgs e)
        {
            SerializeData();
        }
        public void ChargeVideos() 
        {

            
        }
        public void OnSearchTextChanged(object sender, SearchingSongorVideo e)
        {
            List<Video> resultVideos = new List<Video>();
            List<string> resultString = new List<string>();
            resultVideos = videos.Where(t =>
               t.VideoName.ToUpper().Contains(e.SearchTextSongVideo.ToUpper()) ||
               t.Genre.ToUpper().Contains(e.SearchTextSongVideo.ToUpper()) ||
               t.Category.ToUpper().Contains(e.SearchTextSongVideo.ToUpper()) ||
               t.Actor.ToUpper().Contains(e.SearchTextSongVideo.ToUpper()) ||
               t.Director.ToUpper().Contains(e.SearchTextSongVideo.ToUpper()) ||
               t.Studio.Contains(e.SearchTextSongVideo.ToUpper()) ||
               t.UploadDate.ToShortDateString().Contains(e.SearchTextSongVideo) ||
               t.Description.ToUpper().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Duration.ToString().Contains(e.SearchTextSongVideo.ToUpper()) ||         
               t.Qualification.ToString().Contains(e.SearchTextSongVideo.ToUpper()) ||
               t.Reproduction.ToString().Contains((e.SearchTextSongVideo.ToUpper())) ||
               t.Sexo.ToUpper().Contains(e.SearchTextSongVideo.ToUpper()) ||
               t.Age.ToUpper().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Resolution.ToString().Contains(e.SearchTextSongVideo.ToUpper()))       
               .ToList();
            if (resultVideos.Count > 0)
            {
                resultString.Add("-----Videos encontrados-----");
                foreach (Video s in resultVideos)
                    resultString.Add(s.ToString());
            }
            view.UpdateResultsvideoandsong(resultString);
            SerializeData();
        }
        public string OnSelectedSongVideoEventArgs(object sender, SelectVideoEventArgs e)
        {
            string reproduce = "";
            foreach (Video video in videos)
            {
                if (e.Selectedvideo.Contains(video.VideoName))
                {
                    reproduce = video.Path;
                }
            }
            SerializeData();
            return reproduce;
        }
        public Video OnverifyVideo(object sender, ReturnVideoEventArgs e)
        {
            foreach (Video video in videos)
            {
                if (e.Verifyvideoinvideoofuser.Contains(video.VideoName))
                {
                    return video;
                }
            }
            SerializeData();
            return null;
        }
        public void OnVideocaracteristics(object sender,SendingvideocaracteristicsEventArgs e)
        {
            videos.Add(new Video(e.Videoname, e.Genero, e.Categoria, e.Actor, e.Director, e.Estudio, Convert.ToDateTime(aDate.ToString("dddd, dd MMMM yyyy HH:mm:ss")), e.Descripcion, e.duracion, 0, 0, e.Sexo, e.Edad, e.Resolution,e.path,e.byts));
            SerializeData();
        }
        public bool OnverifyVideoExist(object sender, VideosExistEventsArtgs e)
        {
            foreach (Video video in videos)
            {
                if (video.VideoName == e.VideoNameText)
                {
                    return true;
                }
            }
            SerializeData();
            return false;
        }
        public List<string> OnTotalitsofvideos(object sender, SendingVideosEventArgs e)
        {
            List<string> modeartistsongs = new List<string>();
            foreach (Video video in videos)
            {
                if (video.Actor == e.Sendinguser || video.Director==e.Sendinguser)
                {
                    modeartistsongs.Add(video.VideoName);
                }
            }
            SerializeData();
            return modeartistsongs;
        }
        public void Onvideoreproduction(object sender, ReproduccionesEventArgs e)
        {
            foreach (Video video in videos)
            {
                if (e.Nametext.Contains(video.VideoName))
                {
                    video.Reproduction++;
                }


            }
            SerializeData();
        }
        public void Onqualificationchanged(object sender, MandarcalficacionEventArgs e)
        {

            foreach (Video video in videos)
            {
                if (e.Namecancion.Contains(video.VideoName))
                {
                    video.Qualification = (video.Qualification + e.Calification) / total;
                    total++;
                }


            }
            SerializeData();

        }
        public List<List<List<string>>> Palabras(string Contenido)
        {
            if (!String.IsNullOrEmpty(Contenido))
            {
                List<List<List<string>>> Agregar = new List<List<List<string>>>();

                if (Contenido.Contains(" or "))
                {
                    foreach (string Contenido0 in Contenido.Split(new string[] { " or " }, StringSplitOptions.None))
                    {
                        List<List<string>> another = new List<List<string>>();
                        string Contenido00 = Contenido0.Replace(" ", "");
                        if (Contenido00.Contains("and"))
                        {
                            foreach (string inside in Contenido00.Split(new string[] { "and" }, StringSplitOptions.None))
                            {
                                string[] separacion = inside.Split(new string[] { ":" }, StringSplitOptions.None);
                                another.Add(new List<string> { separacion[0], separacion[1] });
                            }

                        }
                        else
                        {

                            string[] champion = Contenido00.Split(new string[] { "=" }, StringSplitOptions.None);
                            another.Add(new List<string> { champion[0], champion[1] });

                        }

                        Agregar.Add(another);
                    }

                }
                else
                {
                    List<List<string>> otro = new List<List<string>>();
                    if (Contenido.Contains("and"))
                    {
                        foreach (string caract in Contenido.Split(new string[] { "and" }, StringSplitOptions.None))
                        {
                            string caract00 = caract.Replace(" ", "");

                            string[] caract000 = caract00.Split(new string[] { "=" }, StringSplitOptions.None);
                            otro.Add(new List<string> { caract000[0], caract000[1] });

                        }
                    }
                    else
                    {
                        string[] champion = Contenido.Split(new string[] { "=" }, StringSplitOptions.None);
                        otro.Add(new List<string> { champion[0], champion[1] });
                    }
                    Agregar.Add(otro);
                }
                return Agregar;
            }
            return null;
        }
        public List<Video> OnBuscarvideo(object sender, SendingtextMultipleFiltersEventArgs e) //deberia retornar una lista de canciones
        {

            List<List<List<string>>> palabras = Palabras(e.TexttoMultipleFilters);
            List<Video> Definitivo = new List<Video>();
            List<List<Video>> optativo = new List<List<Video>>();
            if (palabras != null)
            {
                foreach (List<List<string>> words in palabras)
                {
                    List<Video> cancionesseleccionadas = new List<Video>();
                    foreach (Video video in videos)
                    {
                        int count = 0;
                        foreach (List<string> seleccion in words)
                        {
                            switch (seleccion[0])
                            {
                                case "Nombrevideo":
                                    if (video.VideoName.Trim().ToUpper() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Genero":
                                    if (video.Genre.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {

                                        count++;
                                    }
                                    break;
                                case "Categoria":
                                    if (video.Category.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Actor":
                                    if (video.Actor.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Director":
                                    if (video.Director.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Estudio":
                                    if (video.Studio.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Añodepublicacion":
                                    if (video.UploadDate == Convert.ToDateTime(seleccion[1]))
                                    {
                                        count++;
                                    }
                                    break;
                                case "Descripcion":
                                    if (video.Description.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Duracion":
                                    if (video.Duration == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Calificacion":
                                    if (video.Qualification == Convert.ToInt32(seleccion[1]))
                                    {
                                        count++;
                                    }
                                    break;
                                case "Reproducciones":
                                    if (video.Reproduction == Convert.ToInt32(seleccion[1]))
                                    {
                                        count++;
                                    }
                                    break;
                                case "Sexo":
                                    if (video.Sexo.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Edad":
                                    if (video.Age.Trim() == seleccion[1].Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Resolucion":
                                    if (video.Resolution.Trim() == seleccion[1].Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                default:

                                    break;

                            }

                        }

                        if (count == words.Count)
                        {
                            cancionesseleccionadas.Add(video);
                            count = 0;
                        }
                        optativo.Add(cancionesseleccionadas);
                    }
                }
            }

            if (optativo != null)
            {
                foreach (List<Video> vhs in optativo)
                {
                    foreach (Video netflix in vhs)
                    {
                        if (!Definitivo.Contains(netflix))
                        {
                            Definitivo.Add(netflix);
                        }
                    }
                }
            }
            return Definitivo;
        }
    }
}
