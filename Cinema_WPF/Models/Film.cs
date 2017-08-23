using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_WPF.Models
{
    class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desription { get; set; }
        public int Year { get; set; }

        public virtual ICollection<Director> Directors { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }

        public Film()
        {
            Directors = new List<Director>();
            Genres = new List<Genre>();
            Sessions = new List<Session>();
        }
    }
}
