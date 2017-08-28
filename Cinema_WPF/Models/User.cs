using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Mapping;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cinema_WPF.Helper;
using GalaSoft.MvvmLight;

namespace Cinema_WPF.Models
{
    public class User : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public int? UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public User()
        {
            Tickets = new List<Ticket>();
        }

        
        private string password { get; set; }

        public string Password
        {
            set { password = Cryptograph.Encrypt(value); }
            get { return password; }
        }
             /*{ return Cryptograph.Decrypt(password); }*/


        
    }
}
