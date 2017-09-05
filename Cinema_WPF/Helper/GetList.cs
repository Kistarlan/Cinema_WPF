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

        public static ObservableCollection<Director> GetDirectors(DbSet<Director> directors, int start, int size)
        {
            ObservableCollection<Director> rezult = new ObservableCollection<Director>();
            List<Director> Directors = directors.ToList();
            int count = Directors.Count();
            if (size < count - start)
            {
                for (int i = start; i < start + size; i++)
                {
                    rezult.Add(Directors[i]);
                }
                return rezult;
            }
            else
            {
                for (int i = start; i < count; i++)
                {
                    rezult.Add(Directors[i]);
                }
                return rezult;
            }
        }

        public static ObservableCollection<Director> AddDirector(ObservableCollection<Director> directors, Director director)
        {
            ObservableCollection<Director> rezult = new ObservableCollection<Director>();
            if (directors == null)
            {
                rezult.Add(director);
                return rezult;
            }
            else
            {
                List<Director> Directors = directors.ToList();
                int count = Directors.Count;
                for (int i = 0; i < count; i++)
                {
                    rezult.Add(Directors[i]);
                }
                rezult.Add(director);
                return rezult;
            }
        }

        public static ObservableCollection<Genre> AddGenre(ObservableCollection<Genre> genres, Genre genre)
        {
            ObservableCollection<Genre> rezult = new ObservableCollection<Genre>();
            if (genres == null)
            {
                rezult.Add(genre);
                return rezult;
            }
            else
            {
                List<Genre> Genres = genres.ToList();
                int count = Genres.Count;
                for (int i = 0; i < count; i++)
                {
                    rezult.Add(Genres[i]);
                }
                rezult.Add(genre);
                return rezult;
            }
        }

        public static ObservableCollection<Genre> RemoveGenre(ObservableCollection<Genre> genres, Genre genre)
        {
            ObservableCollection<Genre> rezult = new ObservableCollection<Genre>();
            List<Genre> Genres = genres.ToList();
            int count = Genres.Count();
            for (int i = 0; i < count; i++)
            {
                if (Genres[i].Id != genre.Id)
                    rezult.Add(Genres[i]);
            }
            return rezult;
        }

        public static ObservableCollection<Film> AddFilm(ObservableCollection<Film> films, Film film)
        {
            ObservableCollection<Film> rezult = new ObservableCollection<Film>();
            if (films == null)
            {
                rezult.Add(film);
                return rezult;
            }
            else
            {
                List<Film> Films = films.ToList();
                int count = films.Count;
                for (int i = 0; i < count; i++)
                {
                    rezult.Add(Films[i]);
                }
                rezult.Add(film);
                return rezult;
            }
        }

        public static ObservableCollection<Film> RemoveFilm(ObservableCollection<Film> films, Film film)
        {
            ObservableCollection<Film> rezult = new ObservableCollection<Film>();
            List<Film> Films = films.ToList();
            int count = Films.Count();
            for (int i = 0; i < count; i++)
            {
                if (Films[i].Id != film.Id)
                    rezult.Add(Films[i]);
            }
            return rezult;
        }

        public static ObservableCollection<Director> RemoveDirector(ObservableCollection<Director> directors, Director director)
        {
            ObservableCollection<Director> rezult = new ObservableCollection<Director>();
            List<Director> Directors = directors.ToList();
            int count = Directors.Count();
            for (int i = 0; i < count; i++)
            {
                if(Directors[i].Id != director.Id)
                    rezult.Add(Directors[i]);
            }
            //rezult.Add(director);
            return rezult;

        }

        public static void GetDirectorsforFilmChanging(ObservableCollection<Director> directors, Film film, ObservableCollection<Director> CurrentDirectors, ObservableCollection<Director> Directors)
        {
            List<Director> DirectorList = directors.ToList();
            var FilmDirectorList = film.Directors.ToList();
            int count = DirectorList.Count;

            for (int i = 0; i < count; i++)
            {

                if (FilmDirectorList.Find(s => s.Id == DirectorList[i].Id) != null)
                {
                    CurrentDirectors.Add(DirectorList[i]);
                }
                else
                {
                    Directors.Add(DirectorList[i]);
                }
            }
        }

        public static void GetGenresforFilmChanging(ObservableCollection<Genre> genres, Film film, ObservableCollection<Genre> CurrentGenres, ObservableCollection<Genre> Genres)
        {
            var GenreList = genres.ToList();
            var FilmDirectorList = film.Genres.ToList();
            int count = GenreList.Count;

            for (int i = 0; i < count; i++)
            {

                if (FilmDirectorList.Find(s => s.Id == GenreList[i].Id) != null)
                {
                    CurrentGenres.Add(GenreList[i]);
                }
                else
                {
                    Genres.Add(GenreList[i]);
                }
            }
        }

        public static void GetFilmsFromDirector(ObservableCollection<Film> films, Director director, ObservableCollection<Film> CurrentFilms, ObservableCollection<Film> Films)
        {
            var FilmList = films.ToList();
            var DirectorFilmList = director.Films.ToList();
            int count = FilmList.Count;

            for (int i = 0; i < count; i++)
            {

                if (DirectorFilmList.Find(s => s.Id == FilmList[i].Id) != null)
                {
                    CurrentFilms.Add(FilmList[i]);
                }
                else
                {
                    Films.Add(FilmList[i]);
                }
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

        public static int GetDirectorPages(DbSet<Director> directors, int size)
        {
            int remainder = directors.Count() % size;
            if (directors.Count() / size == 0)
                return 1;
            if (remainder == 0)
                return directors.Count() / size;
            else
                return directors.Count() / size + 1;
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
