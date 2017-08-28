using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Cinema_WPF.Models
{
    public class Genre : ObservableObject
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
