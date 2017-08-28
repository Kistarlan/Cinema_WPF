using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Cinema_WPF.Models
{
    public class Ticket : ObservableObject
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public int? SessionId { get; set; }
        public virtual Session Session { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
