using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cinema_WPF.Context;
using Cinema_WPF.Models;

namespace Cinema_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CinemaContext dbContext;
        public MainWindow()
        {
            dbContext = new CinemaContext();
            UserRole AdminRole = new UserRole() { Name = "Admin" };
                UserRole CashierRole = new UserRole() { Name = "Cashier" };
                dbContext.UserRoles.Add(AdminRole);
                dbContext.UserRoles.Add(CashierRole);
                //dbContext.UserRoles.AddRange(new List<UserRole>() { AdminRole, CashierRole });
                dbContext.SaveChanges();
                User AdminUser = new User() { Login = "Admin", Name = "Admin", Surname = "Admin", Password = "Admin", UserRole = AdminRole, UserRoleId = AdminRole.Id };
                dbContext.Users.Add(AdminUser);
                dbContext.SaveChanges();
            InitializeComponent();
        }
    }
}
