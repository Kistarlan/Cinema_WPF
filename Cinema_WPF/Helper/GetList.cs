using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cinema_WPF.Models;

namespace Cinema_WPF.Helper
{
    class GetList
    {
        public static ObservableCollection<Film> GetFilms(DbSet<Film> films, int start, int size)
        {
            ObservableCollection<Film> rezult = new ObservableCollection<Film>();
            List<Film> Films = films.ToList();
            int count = Films.Count();
            if (size < count - start)
            {
                for (int i = start; i < start+size; i++)
                {
                    rezult.Add(Films[i]);
                }
                return rezult;
            }
            else
            {
                for (int i = start; i < count; i++)
                {
                    rezult.Add(Films[i]);
                }
                return rezult;
            }
        }

        public static ObservableCollection<Session> Get_Sessions(List<Session> sessions, int start, int size)
        {

            ObservableCollection<Session> rezult = new ObservableCollection<Session>();
            List<Session> Sessions = sessions;
            int count = Sessions.Count();
            Session.Sort(Sessions);
            if (size < count - start)
            {
                for (int i = start; i < start + size; i++)
                {
                    rezult.Add(Sessions[i]);
                }
                return rezult;

            }
            else
            {
                for (int i = start; i < count; i++)
                {
                    rezult.Add(Sessions[i]);
                }
                return rezult;
            }
        }

        public static ObservableCollection<Ticket> GetTickets(Session session)
        {

            ObservableCollection<Ticket> rezult = new ObservableCollection<Ticket>();
            var tickets = session.Tickets.ToList();
            int count = session.Tickets.Count();
            SortTickets(tickets);
            for (int i = 0; i < count; i++)
            {
                rezult.Add(tickets[i]);
            }
            return rezult;
        }

        public static int GetFilmPages(DbSet<Film> films, int size)
        {
            int remainder = films.Count() % size;
            if (films.Count() / size == 0)
                return 1;
            if(remainder == 0)
                return films.Count()/size;
            else
                return films.Count()/size + 1;
        }

        public static int GetSessionPages(List<Session> sessions, int size)
        {
            int remainder = sessions.Count() % size;
            if (sessions.Count / size == 0)
                return 1;
            if (remainder == 0)
                return sessions.Count() / size;
            else
                return sessions.Count() / size + 1;
        }

        public static void SortTickets(List<Ticket> tickets)
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                for (int j = 1; j < tickets.Count-i; j++)
                {
                    if (tickets[j].Row < tickets[j - 1].Row)
                    {
                        var t = tickets[j];
                        tickets[j] = tickets[j - 1];
                        tickets[j - 1] = t;
                        continue;
                    }
                    if(tickets[j].Row == tickets[j - 1].Row)
                        if (tickets[j].Column < tickets[j - 1].Column)
                        {
                            var t = tickets[j];
                            tickets[j] = tickets[j - 1];
                            tickets[j - 1] = t;
                            continue;
                        }
                }
            }
        }
    }
}
