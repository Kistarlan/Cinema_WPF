using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Cinema_WPF.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Visibility IncorrectLoginVisibility { get; set; } = Visibility.Collapsed;
        public Visibility IncorrectPasswordlVisibility { get; set; } = Visibility.Collapsed;
        public ICommand LoginCommand { get; private set; }


        public MainWindowViewModel()
        {
            LoginCommand = new RelayCommand(LoginMethod);
        }

        private void LoginMethod()
        {
            
        }
    }
}
