using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;

namespace Cinema_WPF.Models
{
    public class Ticket : ObservableObject
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Exist { get; set; }
        [NotMapped]
        public Visibility Ordered { get; set; }

        public int? SessionId { get; set; }
        public virtual Session Session { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public Ticket()
        {

        }

        public static void SetOrders(Session session)
        {
            var tickets = session.Tickets.ToList();
            for (int i = 0; i < tickets.Count; i++)
            {
                tickets[i].Ordered = Visibility.Visible;
            }
        }
    }
}
