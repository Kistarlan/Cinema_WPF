using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cinema_WPF.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        //public double Width{ get; set; }
        //public double Height{ get; set; }
        public Visibility LoginViewVisibility { get; private set; }
        
        public Visibility MainViewVisibility { get; private set; }
        public MainWindowViewModel()
        {
            LoginViewVisibility = Visibility.Visible;
            MainViewVisibility = Visibility.Hidden;
            VisibilityPropertyChanged();
        }

        public void Login()
        {
            LoginViewVisibility = Visibility.Hidden;
            MainViewVisibility = Visibility.Visible;
            VisibilityPropertyChanged();
        }

        public void Logout()
        {
            LoginViewVisibility = Visibility.Visible;
            MainViewVisibility = Visibility.Hidden;
            VisibilityPropertyChanged();
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
