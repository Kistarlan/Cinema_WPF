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
using Cinema_WPF.Views;

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
        private LoginView loginView;
        private MainView mainView;
        public MainWindow()
        {
            dbContext = new CinemaContext();
            //List<Session> list = dbContext.Sessions.ToList();
            //for (int i = 0; i < list.Count(); i++)
            //{
            //    dbContext.Tickets.RemoveRange(list[i].Tickets);
            //    list[i].GenerateTickets(dbContext);
            //}
            //dbContext.Sessions.RemoveRange(dbContext.Sessions);
            //dbContext.SaveChanges();
            //dbContext.s
            //foreach (var session in dbContext.Sessions)
            //{
            //    session.GenerateTickets();
            //}
            //var loginviewmodel = LoginView.DataContext;
            //UserRole AdminRole = new UserRole() { Name = "Admin" };
            //UserRole CashierRole = new UserRole() { Name = "Cashier" };
            //dbContext.UserRoles.Add(AdminRole);
            //dbContext.UserRoles.Add(CashierRole);
            //dbContext.SaveChanges();
            //User AdminUser = new User() { Login = "Admin", Name = "Admin", Surname = "Admin", Password = "Admin", UserRole = dbContext.UserRoles.First(), UserRoleId = 0, DateOfBirth = Convert.ToDateTime(new DateTime(1997, 3, 12)).Date };
            //dbContext.Users.Add(AdminUser);
            //////dbContext.Users.Remove(dbContext.Users.First());
            //ShowLoginView();
            //Binding loginviewBinding = new Binding();
            //loginviewBinding.Source = loginviewmodel;
            //loginviewBinding.Mode = BindingMode.TwoWay;
            //loginviewBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //loginviewBinding.Path = new PropertyPath("LoginViewVisibility");

            //this.LoginView.SetBinding(VisibilityProperty, loginviewBinding);
            //dbContext.Sessions.Add(new Session()
            //{
            //    DateTime = new DateTime(2017, 9, 1, 15, 30, 0),
            //    Film = dbContext.Films.FirstOrDefault(i => i.Name.ToLower() == "Inception"),
            //    Hall = dbContext.Halls.FirstOrDefault(h => h.Number == 1),
            //    Price = 50
            //});
            //dbContext.Sessions.Add(new Session()
            //{
            //    DateTime = new DateTime(2017, 9, 6, 15, 30, 0),
            //    Film = dbContext.Films.FirstOrDefault(i => i.Name.ToLower() == "Dark Knight"),
            //    Hall = dbContext.Halls.FirstOrDefault(h => h.Number == 3),
            //    Price = 50
            //});
            //dbContext.Sessions.Add(new Session()
            //{
            //    DateTime = new DateTime(2017, 9, 7, 17, 30, 0),
            //    Film = dbContext.Films.FirstOrDefault(i => i.Name.ToLower() == "Dark Knight"),
            //    Hall = dbContext.Halls.FirstOrDefault(h => h.Number == 1),
            //    Price = 50
            //});
            //dbContext.Sessions.Add(new Session()
            //{
            //    DateTime = new DateTime(2017, 9, 7, 15, 30, 0),
            //    Film = dbContext.Films.FirstOrDefault(i => i.Name.ToLower() == "Dark Knight"),
            //    Hall = dbContext.Halls.FirstOrDefault(h => h.Number == 2),
            //    Price = 50
            //});
            //dbContext.Sessions.Add(new Session()
            //{
            //    DateTime = new DateTime(2017, 9, 5, 15, 30, 0),
            //    Film = dbContext.Films.FirstOrDefault(i => i.Name.ToLower() == "Lord of the Rings"),
            //    Hall = dbContext.Halls.FirstOrDefault(h => h.Number == 3),
            //    Price = 50
            //});

            dbContext.SaveChanges();

            loginView = new LoginView();
            mainView = new MainView();
            loginView.Visibility = Visibility.Visible;

            //loginView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.DataContext = new MainWindowViewModel(this);
            mainView.SetBinding(VisibilityProperty,
                new Binding("MainViewVisibility") { Source = this.DataContext, Mode = BindingMode.OneWay });
            loginView.SetBinding(VisibilityProperty,
                new Binding("LoginViewVisibility") { Source = this.DataContext, Mode = BindingMode.OneWay });
            this.Visibility = Visibility.Hidden;
            //InitializeComponent();
            
            //this.Width = 300;
            //this.Height = 400;
            loginviewmodel = loginView.DataContext as LoginViewModel;
            if (loginviewmodel != null)
                loginviewmodel.ParentMainWindowViewModel = this.DataContext as MainWindowViewModel;
           
            MainViewViewModel = mainView.DataContext as MainViewViewModel;
            if (MainViewViewModel != null)
                MainViewViewModel.ParentMainWindowViewModel = this.DataContext as MainWindowViewModel;
            //ShowMainView(dbContext.Users.FirstOrDefault(i => i.Login.ToLower() == "Admin"));
        }

        public void ShowMainView(User user)
        {
            mainView.Visibility = Visibility.Visible;
            //loginView = null;
            MainViewViewModel = mainView.DataContext as MainViewViewModel;
            if (MainViewViewModel != null)
                MainViewViewModel.ParentMainWindowViewModel = this.DataContext as MainWindowViewModel;
            MainViewViewModel.User = user;
            //MainView.Load();
            MainViewViewModel.Show();
            //MainView.Visibility = Visibility.Visible;
            //LoginView.Visibility = Visibility.Hidden;
        }

        public void ShowLoginView()
        {
            //MainView.Visibility = Visibility.Hidden;
            //LoginView.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Normal;
            this.ResizeMode = ResizeMode.NoResize;
            this.MinWidth = 300;
            this.MinHeight = 300;
            this.Width = 300;
            this.Height = 400;
        }

        public void closeAll()
        {
            this.Close();
            loginView?.Close();
            mainView?.Close();
            //Application.Current.Shutdown(0);
        }
    }
}
