using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class PlaylistSong
    {
        List<Song> songs;
        List<Song> random;
        List<bool> preferences;
        string name;

        public PlaylistSong(string playlistname)
        {
            this.songs = new List<Song>();
            this.random = new List<Song>();
            this.preferences = new List<bool>() { false, false }; //privacy, download
            this.name = playlistname;
        }

        // Get
        public bool GetPlaylistPrivacy()
        {
            return preferences[1];
        }
        public bool GetPlaylistDownload()
        {
            return preferences[0];
        }
        public string GetPlaylistName()
        {
            return name;
        }
        public List<Song> GetPlaylistAllSongs()
        {
            return songs;
        }
        public void SetPrivacy(string privacyoption)
        {
            if (privacyoption == "Privada" && name != "Videos Favoritos") // false es privado
            {
                preferences[1] = false;
            }
            else if (privacyoption == "Publica" && name != "Videos Favoritos") // false es privado
            {
                preferences[1] = true;
            }
        }

        public bool ChangePrivacy(bool input)
        {
            if (preferences[1] == true)
            {
                preferences[1] = false; // queda privada
                return false;
            }
            if (preferences[1] == false)
            {
                preferences[1] = true; // queda publica
                return true;
            }
            return false; // no paso nada por defecto setea la playlist a privada
        }

        public bool DownloadPlaylist(string premium) // ver si se cambia la variable premium
        {
            if (premium == "Premium")
            {
                if (preferences[0] == true)
                {
                    preferences[0] = false; // queda privada
                    return false;
                }
                if (preferences[0] == false)
                {
                    preferences[0] = true; // queda publica
                    return true;
                }
                return false;
            }
            else
            {
                return false; // no pasa nada por defecto no descarga la playlist
            }
        }

        public void AddSong(Song song)
        {
            bool flag = false;
            foreach (Song value in this.songs)
            {
                if (value.Namesong == song.Namesong)
                {
                    flag = true;
                    // no agrega la cancion porque ya está
                }
            }
            if (flag == false)
            {
                songs.Add(song);
            }
            // si agrego la cancion
        }

        public bool RemoveSong(Song song)
        {
            foreach (Song value in songs)
            {
                if (value.Namesong == song.Namesong)
                {
                    songs.Remove(song);
                    return true; // La cancion es eliminada
                }
            }
            return false; // La cancion no fue encontrada
        }

        // queue
        public bool CheckSongsInQueue()
        {
            if (0 < random.Count())
            {
                return true; // Hay canciones en Queue
            }
            return false;
        }

        public List<Song> NextOnQueue()
        {
            List<Song> newlist = new List<Song>();
            int counter = 0;
            foreach (Song elemntsong in random)
            {
                if (counter != 0)
                {
                    newlist.Add(elemntsong);
                }
                counter++;
            }
            return random = newlist;
        }

        public bool AddToQueue(Song song)
        {
            List<Song> newlist = new List<Song>();
            newlist.Add(song);
            foreach (Song elemntsong in random)
            {
                newlist.Add(elemntsong);
            }
            random = newlist;
            return true; // Se agrega cancion a la queue
        }

        public bool RemoveFromQueue(string songName)
        {
            if (0 < random.Count())
            {
                foreach (Song value in random)
                {
                    if (value.Namesong == songName)
                    {
                        random.Remove(value);
                        return true; // Cancion eliminada
                    }
                }
            }
            return false; // No se elimino la cancion o no hay canciones en queue
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max); // Devuelve un numero random
        }

        public List<Song> RandomPlaylistOrder(string selectedsong)
        {
            random.Clear();
            List<Song> songlist = new List<Song>();
            foreach (Song song in songs)
            {
                if (song.Namesong != selectedsong)
                {
                    songlist.Add(song);
                }
            }
            int counter = songlist.Count();

            for (int i = 0; i < songs.Count() - 1; i++)
            {
                int rnd = RandomNumber(0, counter);
                random.Add(songlist[rnd]);
                songlist.RemoveAt(rnd);
                counter--;
            }

            return random; // devualve una mezcla de canciones de esta playlist
        }

        public List<Song> GetQueue()
        {
            return random;
        }

        public bool ClearQueue()
        {
            random.Clear();
            return true; // Queue Cleared
        }
    }
}
