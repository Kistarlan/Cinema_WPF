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
using Cinema_WPF.ViewModels;

namespace Cinema_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private CinemaContext dbContext;
        LoginViewModel loginviewmodel;
        MainViewViewModel MainViewViewModel;
        public MainWindow()
        {

            //var loginviewmodel = LoginView.DataContext;
            //dbContext = new CinemaContext();
            //UserRole AdminRole = new UserRole() { Name = "Admin" };
            //UserRole CashierRole = new UserRole() { Name = "Cashier" };
            //dbContext.UserRoles.Add(AdminRole);
            //dbContext.UserRoles.Add(CashierRole);
            //dbContext.SaveChanges();
            //User AdminUser = new User() { Login = "Admin", Name = "Admin", Surname = "Admin", Password = "Admin", UserRole = dbContext.UserRoles.First(), UserRoleId = 0, DateOfBirth = Convert.ToDateTime(new DateTime(1997, 3, 12)).Date };
            //dbContext.Users.Add(AdminUser);
            //////dbContext.Users.Remove(dbContext.Users.First());
            //dbContext.SaveChanges();
            //ShowLoginView();
            //Binding loginviewBinding = new Binding();
            //loginviewBinding.Source = loginviewmodel;
            //loginviewBinding.Mode = BindingMode.TwoWay;
            //loginviewBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //loginviewBinding.Path = new PropertyPath("LoginViewVisibility");

            //this.LoginView.SetBinding(VisibilityProperty, loginviewBinding);

            InitializeComponent();
            loginviewmodel = LoginView.DataContext as LoginViewModel;
            loginviewmodel.ParentMainWindowViewModel = this.DataContext as MainWindowViewModel;
            MainViewViewModel = MainView.DataContext as MainViewViewModel;
            MainViewViewModel.ParentMainWindowViewModel = this.DataContext as MainWindowViewModel;
        }

        internal void ShowMainView(User user)
        {
            MainView.Visibility = Visibility.Visible;
            LoginView.Visibility = Visibility.Hidden;
        }

        internal void ShowLoginView()
        {
            MainView.Visibility = Visibility.Hidden;
            LoginView.Visibility = Visibility.Visible;
        }
    }
}
