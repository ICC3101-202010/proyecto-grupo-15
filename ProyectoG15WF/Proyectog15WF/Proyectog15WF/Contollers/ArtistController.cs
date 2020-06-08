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
    public class ArtistController
    {
        List<Artist> artists = new List<Artist>();
        AppForm view;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        public ArtistController(Form view)
        {
            this.view = view as AppForm;
            this.view.Artistifosend += OnCreateArtist;
            this.view.Artistinfo += OnArtistinfo;
            this.view.getArtist += OnGetArtist;
            DeserializeData();
        }

        public void SerializeData()
        {
            try
            {
                FileStream FS = new FileStream("Artist.Bin", FileMode.Create, FileAccess.Write, FileShare.None);
                binaryFormatter.Serialize(FS, artists);
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
                FileStream FS = new FileStream("Artist.bin", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                artists = (List<Artist>)binaryFormatter.Deserialize(FS);
                FS.Close();

            }
            catch
            {

            }
        }
        public void OnCreateArtist(object sender, SendingArtistInfo e)
        {
            artists.Add(new Artist(e.Usernametext, e.AgeArtist, e.GenderArtist, e.ArtistText));
            SerializeData();
        }
        public Artist OnGetArtist(object sender, GetArtistEventArgs e)
        {
            foreach (Artist artist in artists)
            {
                if (artist.Name.ToUpper() == e.ArtistName.ToUpper())
                {
                    return artist;
                }
            }
            return null;
        }
        public void OnArtistinfo(object sender,ArtistInfoEventArgs e)
        {
            {
                List<Artist> resultArtist = new List<Artist>();
                List<string> resultString = new List<string>();
                resultArtist = artists.Where(t =>
                   t.Name.ToUpper().Contains(e.ArtistText.ToUpper()))
               .ToList();
                if (resultArtist.Count > 0)
                {
                    resultString.Add("-----Artistas encontrados-----");
                    foreach (Artist s in resultArtist)
                        resultString.Add(s.ToString());
                }
                view.UpdateResultsArtist(resultString);
                view.UpdateResultsArtistAdmin(resultString);
                SerializeData();
            }
        }
    }
}
