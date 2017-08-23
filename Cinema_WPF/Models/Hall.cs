using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_WPF.Models
{
    class Hall
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
