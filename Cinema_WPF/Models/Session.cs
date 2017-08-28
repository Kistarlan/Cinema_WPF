using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Cinema_WPF.Models
{
    public class Session : ObservableObject
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Price { get; set; }

        public int? FilmId { get; set; }
        public virtual Film Films { get; set; }

        public int? HallId { get; set; }
        public virtual Film Halls { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public Session()
        {
            Tickets = new List<Ticket>();
        }
    }
}
