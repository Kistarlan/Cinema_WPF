using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Cinema_WPF.Models
{
    public class UserRole : ObservableObject
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
