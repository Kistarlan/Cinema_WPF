using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_WPF.Models
{
    class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Film> Films { get; set; }

        public Genre()
        {
            Films = new List<Film>();
        }
    }
}
