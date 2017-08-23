using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_WPF.Models
{
    class UserRole
    {
        
        public int Id { set; get; }
        public string Name { set; get; }

        public virtual ICollection<User> Users { get; set; }

        public UserRole()
        {
            Users = new List<User>();
        }
    }
}
