using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Cinema_WPF.Models
{
    public class Director : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Film> Films { get; set; }

        public Director()
        {
            Films = new List<Film>();
        }
    }
}
