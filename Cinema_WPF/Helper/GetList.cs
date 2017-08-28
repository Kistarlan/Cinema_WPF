using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static int GetFilmPages(DbSet<Film> films, int size)
        {
            int remainder = films.Count() % size;

            if(remainder == 0)
                return films.Count()/size;
            else
                return films.Count()/size + 1;
        }

    }
}
