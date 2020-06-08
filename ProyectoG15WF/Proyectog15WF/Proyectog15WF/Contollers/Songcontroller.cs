using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Proyectog15WF;
using System.Windows.Forms;
using CustomEventArgs;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Proyectog15WF.Contollers
{
    public class Songcontroller
    {
        List<Song> songs = new List<Song>();
        AppForm view;
        string curDir = Directory.GetCurrentDirectory();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        DateTime aDate = DateTime.Now;
        int total = 1;
        public Songcontroller(Form view)
        {
            Chargesong();
            this.view = view as AppForm;
            this.view.Searchingnamevideoorsong += OnSearchTextChanged;
            this.view.Reproducesong += OnSelectedSongVideoEventArgs;
            this.view.Recivingsong += OnverifySong;
            this.view.Songcaracteristics += OnrecivingSongCaracteristics;
            this.view.Totalitsofsongs += OnTotalitsofsongs;
            this.view.verfyedsong += OnverifySongExist;
            this.view.Recivesongmultiplecriteria += OnBuscar;
            this.view.Reproduccionesname += Onsongreproduction;
            this.view.Calificaciondelusuario += Onqualificationchanged;
            DeserializeData();

        }
        public void SerializeData()
        {
            try
            {
                FileStream FS = new FileStream("Songs.Bin", FileMode.Create, FileAccess.Write, FileShare.None);
                binaryFormatter.Serialize(FS, songs);
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
                FileStream FS = new FileStream("Songs.bin", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                songs = (List<Song>)binaryFormatter.Deserialize(FS);
                FS.Close();

            }
            catch
            {

            }
        }


        public void Chargesong()
        {

           

        }
        public void OnSearchTextChanged(object sender, SearchingSongorVideo e)
        {
            List<Song> resultSongs = new List<Song>();
            List<string> resultString = new List<string>();
            resultSongs = songs.Where(t =>
               t.Namesong.ToUpper().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Genre.ToUpper().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Composer.ToUpper().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Discography.ToUpper().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Studio.ToUpper().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Publicationyear.ToShortDateString().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Lyrics.ToUpper().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Duration.ToString().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Category.ToUpper().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Qualification.ToString().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Reproduction.ToString().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Sexo.ToUpper().Contains(e.SearchTextSongVideo.ToUpper())||
               t.Age.ToUpper().Contains(e.SearchTextSongVideo.ToUpper()))
           .ToList();
            if (resultSongs.Count > 0)
            {
                resultString.Add("-----Canciones encontradas-----");
                foreach (Song s in resultSongs)
                    resultString.Add(s.ToString());
            }
            view.UpdateResultsvideoandsong(resultString);
            SerializeData();


        }
        public string OnSelectedSongVideoEventArgs(object sender, SelectSongEventArgs e)
        {
            string reproduce = "";
            foreach (Song song in songs)
            {
                if (e.Selectedsong.Contains(song.Namesong))
                {
                    reproduce = song.Path;
                }
            }
            SerializeData();
            return reproduce;


        }
        public Song OnverifySong(object sender, ReturnsongEventArgs e)
        {   
            foreach(Song cancion  in songs)
            {
                if (e.Verifysonginsongofuser.Contains(cancion.Namesong))
                {
                    return cancion;
                }
            }
            return null;
        }
        public void OnrecivingSongCaracteristics(object sender, SendingsongcaracteristicsEventArgs e)
        {
            songs.Add(new Song(e.Nombrecancion, e.Genero, e.Compositor, e.Discografia, e.Estudio, Convert.ToDateTime(aDate.ToString("dddd, dd MMMM yyyy HH:mm:ss")), e.Letra, e.duracion, e.Categoria, 0, 0, e.Sexo, e.Edad,e.path,e.byts));
            SerializeData();
        }

        public bool OnverifySongExist(object sender, SongsExistEventsArtgs e)
        {
            foreach (Song song in songs)
            {
                if (song.Namesong == e.SongName)
                {
                    return true;
                }
            }
            SerializeData();
            return false;
        }

        public List<string> OnTotalitsofsongs(object sender, SendingSongsEventArgs e)
        { List<string> modeartistsongs = new List<string>();
            foreach (Song song in songs)
            {
                if (song.Composer == e.Sendinguser)
                {
                    modeartistsongs.Add(song.Namesong);
                }
            }
            SerializeData();
            return modeartistsongs;
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
                        if (champion != null)
                        {
                            otro.Add(new List<string> { champion[0], champion[1] });
                        }
                    }
                    Agregar.Add(otro);
                }

                return Agregar;
            }
            return null;
        }

        public List<Song> OnBuscar(object sender, SendingtextMultipleFiltersEventArgs e) //deberia retornar una lista de canciones
        {
         
            List<List<List<string>>> palabras = Palabras(e.TexttoMultipleFilters);
            List<Song> Definitivo = new List<Song>();
            List<List<Song>> optativo = new List<List<Song>>();
            if (palabras != null)
            {
                foreach (List<List<string>> words in palabras)
                {
                    List<Song> cancionesseleccionadas = new List<Song>();
                    foreach (Song song in songs)
                    {
                        int count = 0;
                        foreach (List<string> seleccion in words)
                        {
                            switch (seleccion[0])
                            {
                                case "Nombrecancion":
                                    if (song.Namesong.Trim().ToUpper() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Genero":
                                    if (song.Genre.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {

                                        count++;
                                    }
                                    break;
                                case "Compositor":
                                    if (song.Composer.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Discografia":
                                    if (song.Discography.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Estudio":
                                    if (song.Studio.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Añodepublicacion":
                                    if (song.Publicationyear == Convert.ToDateTime(seleccion[1]))
                                    {
                                        count++;
                                    }
                                    break;
                                case "Letra":
                                    if (song.Lyrics == seleccion[1])
                                    {
                                        count++;
                                    }
                                    break;
                                case "Duracion":
                                    if (song.Duration == seleccion[1])
                                    {
                                        count++;
                                    }
                                    break;
                                case "Categoria":
                                    if (song.Category.ToUpper() == seleccion[1].ToUpper())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Calificacion":
                                    if (song.Qualification == Convert.ToInt32(seleccion[1]))
                                    {
                                        count++;
                                    }
                                    break;
                                case "Reproducciones":
                                    if (song.Reproduction == Convert.ToInt32(seleccion[1]))
                                    {
                                        count++;
                                    }
                                    break;
                                case "Sexo":
                                    if (song.Sexo.ToUpper().Trim() == seleccion[1].ToUpper().Trim())
                                    {
                                        count++;
                                    }
                                    break;
                                case "Edad":
                                    if (song.Age == seleccion[1])
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
                            cancionesseleccionadas.Add(song);
                            count = 0;
                        }
                        optativo.Add(cancionesseleccionadas);


                    }


                }
            }
            if (optativo != null)
            {
                foreach (List<Song> mp3 in optativo)
                {
                    foreach (Song mp4 in mp3)
                    {
                        if (!Definitivo.Contains(mp4))
                        {
                            Definitivo.Add(mp4);
                        }
                    }
                }
            }
            return Definitivo;
        }

        public void Onsongreproduction(object sender ,ReproduccionesEventArgs e)
        {
            foreach (Song song in songs)
            {
                if (e.Nametext.Contains(song.Namesong))
                {
                    song.Reproduction++;
                }


            }
            SerializeData();

        }
        public void Onqualificationchanged(object sender ,MandarcalficacionEventArgs e)
        {
            
            foreach (Song song in songs)
            {
                if (e.Namecancion.Contains(song.Namesong))
                {   
                    song.Qualification = (song.Qualification + e.Calification) / total;
                    total++;
                }


            }
            SerializeData();

        }


    }
  

}
