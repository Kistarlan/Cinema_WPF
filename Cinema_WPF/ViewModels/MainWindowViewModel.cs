using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cinema_WPF.Models;

namespace Cinema_WPF.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        //public double Width{ get; set; }
        //public double Height{ get; set; }
        public MainWindow CurentWindow;
        public Visibility LoginViewVisibility { get; private set; }
        
        public Visibility MainViewVisibility { get; private set; }
        public MainWindowViewModel(MainWindow curentWindow)
        {
            CurentWindow = curentWindow;
            LoginViewVisibility = Visibility.Visible;
            MainViewVisibility = Visibility.Hidden;
            VisibilityPropertyChanged();
        }

        public void Login(User user)
        {
            CurentWindow.ShowMainView(user);
            //LoginViewVisibility = Visibility.Hidden;
            //MainViewVisibility = Visibility.Visible;
            //VisibilityPropertyChanged();
        }

        public void Logout()
        {
            CurentWindow.ShowLoginView();
            //LoginViewVisibility = Visibility.Visible;
            //MainViewVisibility = Visibility.Hidden;
            //VisibilityPropertyChanged();
        }

        private void VisibilityPropertyChanged()
        {
            RaisePropertyChanged("LoginViewVisibility");
            RaisePropertyChanged("MainViewVisibility");
            //RaisePropertyChanged("Width");
            //RaisePropertyChanged("Height");
        }

    }
}
