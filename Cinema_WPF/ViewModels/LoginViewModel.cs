using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;
using Cinema_WPF.Models;
using Cinema_WPF.Context;
using Cinema_WPF.Views;
using Cinema_WPF.Helper;
using MahApps.Metro.Controls;

namespace Cinema_WPF.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        private Window ParentWindow { get; set; }

        public MainWindowViewModel ParentMainWindowViewModel { get; set; }
        private CinemaContext dbContext;
        public string UserName { get; set; }    //Login
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Year { get; set; }
        public string Mounth { get; set; }
        public string Day { get; set; }


        public Visibility IncorrectLoginVisibility { get; set; }
        public Visibility IncorrectRegistrationLoginVisibility { get; set; }
        public Visibility IncorrectRegistrationPasswordVisibility { get; set; }
        public Visibility IncorrectPasswordVisibility { get; set; }
        public Visibility LoginVisibility { get; set; }
        public Visibility RegistrationVisibility { get; set; }
        public Visibility CalendarErrorVisibility { get; set; }
            
        public ICommand LoginCommand { get; private set; }
        public ICommand RegistrationCommand { get; private set; }
        public ICommand RegistrationViewCommand { get; private set; }
        public ICommand LoginViewCommand { get; private set; }
        public ICommand CalendarShowCommand { get; private set; }


        public LoginViewModel()
        {
         
            IncorrectRegistrationLoginVisibility = Visibility.Hidden;
            IncorrectRegistrationPasswordVisibility = Visibility.Hidden;
            RegistrationVisibility = Visibility.Hidden;
            IncorrectPasswordVisibility = Visibility.Hidden;
            IncorrectLoginVisibility = Visibility.Hidden;
            CalendarErrorVisibility = Visibility.Hidden;
            LoginCommand = new RelayCommand<PasswordBox>(LoginMethod);
            RegistrationCommand = new RelayCommand<PasswordBox>(RegistrationMethod);
            RegistrationViewCommand = new RelayCommand<Window>(RegistrationFormVisibilityMethod);
            LoginViewCommand = new RelayCommand(LoginFormVisibilityMethod);

        }

        private void LoginMethod(PasswordBox pb)
        {
            dbContext = new CinemaContext();
            Password = pb.Password;
            var password = Cryptograph.Encrypt(Password);
            var user = dbContext.Users.FirstOrDefault(i => i.Login.ToLower() == this.UserName.ToLower() && i.Password == password);
            if (user == null)
            {
                IncorrectPasswordVisibility = Visibility.Visible;
                PropertyChangedVisibility();
            }
            else
            {
                pb.Password = null;
                CleanAll();
                ParentMainWindowViewModel.Login(user);
            }
        }

        public void Close()
        {
            ParentMainWindowViewModel.Close();
        }

        private void CleanAll()
        {
            UserName = null;
            Password = null;
            Name = null;
            Surname = null;
            Year = null;
            Mounth = null;
            Day = null;

            IncorrectPasswordVisibility = Visibility.Hidden;
            IncorrectLoginVisibility = Visibility.Hidden;
            LoginVisibility = Visibility.Visible;
            RegistrationVisibility = Visibility.Hidden;
            IncorrectRegistrationLoginVisibility = Visibility.Hidden;
            IncorrectRegistrationPasswordVisibility = Visibility.Hidden;
            PropertyChangedVisibility();
            PropertyChangedText();
        }


        private void PropertyChangedText()
        {
            RaisePropertyChanged("UserName");
            RaisePropertyChanged("Password");
            RaisePropertyChanged("Name");
            RaisePropertyChanged("Surname");
            RaisePropertyChanged("Year");
            RaisePropertyChanged("Mounth");
            RaisePropertyChanged("Day");
        }

        private void RegistrationMethod(PasswordBox pb)
        {
            if (UserName.Length < 4)
            {
                IncorrectRegistrationLoginVisibility = Visibility.Visible;
                PropertyChangedVisibility();
                return;
            }
            else
            {
                IncorrectRegistrationLoginVisibility = Visibility.Hidden;
                PropertyChangedVisibility();
            }

            if(pb.Password.Length < 8)
            {
                IncorrectRegistrationPasswordVisibility = Visibility.Visible;
                PropertyChangedVisibility();
                return;
            }
            else
            {
                IncorrectRegistrationPasswordVisibility = Visibility.Hidden;
                PropertyChangedVisibility();
            }
            Password = pb.Password;

            int day = Int32.Parse(Day);
            int mounth = Int32.Parse(Mounth);
            int year = Int32.Parse(Year);

            if((day > 31 || day < 1) || (mounth > 12 || mounth < 1) || (year > 2017 || year < 1920))
            {
                CalendarErrorVisibility = Visibility.Visible;
                PropertyChangedVisibility();
                return;
            }
            else
            {
                CalendarErrorVisibility = Visibility.Hidden;
                PropertyChangedVisibility();
            }

            dbContext = new CinemaContext();
            User user = new User();
            user.Name = Name;
            user.Surname = Surname;
            user.Password = Password;
            user.Login = UserName;
            user.Password = Password;
            user.DateOfBirth = new DateTime(year, mounth, day);
            user.UserRole = dbContext.UserRoles.Find(2);
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            CleanAll();
            ParentMainWindowViewModel.Login(user);
        }

        private void RegistrationFormVisibilityMethod( Window currentWindow)
        {
            if (ParentWindow == null)
                ParentWindow = currentWindow;
            ParentWindow.Height += 150;
            IncorrectPasswordVisibility = Visibility.Hidden;
            IncorrectLoginVisibility = Visibility.Hidden;
            LoginVisibility = Visibility.Hidden;
            RegistrationVisibility = Visibility.Visible;
            PropertyChangedVisibility();
        }
        private void LoginFormVisibilityMethod()
        {
            ParentWindow.Height -= 150;
            IncorrectRegistrationLoginVisibility = Visibility.Hidden;
            IncorrectRegistrationPasswordVisibility = Visibility.Hidden;
            CalendarErrorVisibility = Visibility.Hidden;
            RegistrationVisibility = Visibility.Hidden;
            LoginVisibility = Visibility.Visible;
            PropertyChangedVisibility();
        }

        private void PropertyChangedVisibility()
        {
            RaisePropertyChanged("RegistrationVisibility");
            RaisePropertyChanged("LoginVisibility");
            RaisePropertyChanged("IncorrectLoginVisibility");
            RaisePropertyChanged("IncorrectPasswordVisibility");
            RaisePropertyChanged("CalendarErrorVisibility");
            RaisePropertyChanged("IncorrectRegistrationLoginVisibility");
            RaisePropertyChanged("IncorrectRegistrationPasswordVisibility");
        }



    }
}
