using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class Artist
    {
        string name;
        string age;
        string gender;
        List<Video> videos;
        List<Song> songs;
        List<User> followingUsers;
        string artist;

        public Artist(string name, string age, string gender, string artist)
        {
            this.name = name;
            this.age = age;
            this.gender = gender;
            this.artist = artist;
        }

        public string Name { get => name; set => name = value; }
        public string Age { get => age; set => age = value; }
        public string Gender { get => gender; set => gender = value; }
        public List<Video> Videos { get => videos; set => videos = value; }
        public List<Song> Songs { get => songs; set => songs = value; }
        public List<User> FollowingUsers { get => followingUsers; set => followingUsers = value; }
        public string Artisttype { get => artist; set => artist = value; }

        public override string ToString()
        {
            return name;
        }

    }
}
