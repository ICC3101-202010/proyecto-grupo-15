using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable()]
    public class Song
    {
         string namesong;
         string genre;
         string composer;
         string discography;
         string studio;
         DateTime publicationyear;
         string lyrics;
         string duration;
         string category;
         int qualification;
         int reproduction;
         string sexo;
         string age;
         string path;
        string byts;

        public Song(string namesong, string genre, string composer, string discography, string studio, DateTime publicationyear, string lyrics, string duration, string category, int qualification, int reproduction, string sexo, string age,string path,string byts)
        {
            this.namesong = namesong;
            this.genre = genre;
            this.composer = composer;
            this.discography = discography;
            this.studio = studio;
            this.publicationyear = publicationyear;
            this.lyrics = lyrics;
            this.duration = duration;
            this.category = category;
            this.qualification = qualification;
            this.reproduction = reproduction;
            this.Sexo = sexo;
            this.Age = age;
            this.path = path;
            this.byts = byts;
        }

        public string Namesong { get => namesong; set => namesong = value; }
        public string Genre { get => genre; set => genre = value; }
        public string Composer { get => composer; set => composer = value; }
        public string Discography { get => discography; set => discography = value; }
        public string Studio { get => studio; set => studio = value; }
        public DateTime Publicationyear { get => publicationyear; set => publicationyear = value; }
        public string Lyrics { get => lyrics; set => lyrics = value; }
        public string Duration { get => duration; set => duration = value; }
        public string Category { get => category; set => category = value; }
        public int Qualification { get => qualification; set => qualification = value; }
        public int Reproduction { get => reproduction; set => reproduction = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public string Age { get => age; set => age = value; }
        public string Path { get => path; set => path = value; }
        public string Byts { get => byts; set => byts = value; }

        public override string ToString()
        {
            return  Namesong;
        }
    }
}
