using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class PlaylistVideo
    {
        List<Video> videos; // Quizas hacer una lista de objetos Song
        List<Video> random; // Quizas hacer una lista de objetos Song
        List<bool> preferences;
        string name;

        public PlaylistVideo(string playlistname)
        {
            this.videos = new List<Video>();
            this.random = new List<Video>();
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
        public List<Video> GetPlaylistAllVideos()
        {
            return videos;
        }
        //

        public void SetPrivacy(string privacyoption)
        {
            if (privacyoption == "Privada" && name != "Musica Favorita") // false es privado
            {
                preferences[1] = false;
            }
            else if (privacyoption == "Publica" && name != "Musica Favorita") // false es privado
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

        public bool AddVideo(Video video)
        {
            foreach (Video value in videos)
            {
                if (value.VideoName == video.VideoName)
                {
                    return false; // no agrega la cancion porque ya está
                }
            }

            videos.Add(video);
            return true; // si agrego la cancion
        }

        public bool RemoveSong(Video video)
        {
            foreach (Video value in videos)
            {
                if (value.VideoName == video.VideoName)
                {
                    videos.Remove(video);
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

        public List<Video> NextOnQueue()
        {
            List<Video> newlist = new List<Video>();
            int counter = 0;
            foreach (Video elemntsong in random)
            {
                if (counter != 0)
                {
                    newlist.Add(elemntsong);
                }
                counter++;
            }
            return random = newlist;
        }

        public bool AddToQueue(Video video)
        {
            random.Add(video);
            return true; // Se agrega cancion a la queue
        }

        public bool RemoveFromQueue(string songName)
        {
            if (0 < random.Count())
            {
                foreach (Video value in random)
                {
                    if (value.VideoName == songName)
                    {
                        random.Remove(value);
                        return true; // Cancion agregada
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
        public List<Video> RandomPlaylistOrder(string selectedsong)
        {
            random.Clear();
            List<Video> videolist = new List<Video>();
            foreach (Video video in videos)
            {
                if (video.VideoName != selectedsong)
                {
                    videolist.Add(video);
                }
            }
            int counter = videolist.Count();

            for (int i = 0; i < videos.Count() - 1; i++)
            {
                int rnd = RandomNumber(0, counter);
                random.Add(videolist[rnd]);
                videolist.RemoveAt(rnd);
                counter--;
            }

            return random; // devualve una mezcla de canciones de esta playlist
        }

        public List<Video> GetQueue()
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


