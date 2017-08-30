using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Cinema_WPF.Context;
using GalaSoft.MvvmLight;

namespace Cinema_WPF.Models
{
    public class Session : ObservableObject
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Price { get; set; }


        public int? FilmId { get; set; }
        public virtual Film Film { get; set; }

        public int? HallId { get; set; }
        public virtual Hall Hall { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public static List<Session> Sort(List<Session> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 1; j < list.Count - i; j++)
                {
                    if (list[j-1].DateTime > list[j].DateTime)
                    {
                        var temp = list[j - 1];
                        list[j - 1] = list[j];
                        list[j] = temp;
                    }
                }
            }


            return list;
        }

        public Session()
        {
            Tickets = new List<Ticket>();
            //GenerateTickets();
        }

        public void GenerateTickets(CinemaContext db)
        {
            if(Hall == null)
                return;
            //using (CinemaContext db = new CinemaContext())
            {
                for (int i = 0; i < Hall.RowCount; i++)
                {
                    for (int j = 0; j < Hall.ColumnCount; j++)
                    {
                        db.Tickets.Add(new Ticket()
                        {
                            Row = i,
                            Column = j,
                            Exist = true,
                            Session = this
                        });
                    }
                }
            }
        }
    }
}
