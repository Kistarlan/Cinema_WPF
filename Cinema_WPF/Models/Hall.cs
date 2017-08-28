using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Cinema_WPF.Models
{
    public class Hall : ObservableObject
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

        public Hall()
        {
            Sessions = new List<Session>();
        }
    }
}
