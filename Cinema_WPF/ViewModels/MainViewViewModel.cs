using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema_WPF.ViewModels
{
    class MainViewViewModel : ViewModelBase
    {
        public MainWindowViewModel ParentMainWindowViewModel { get; set; }

        public ICommand LogoutCommand { get; private set; }
        public MainViewViewModel()
        {
            LogoutCommand = new RelayCommand(Logout);
        }

        private void Logout()
        {
            ParentMainWindowViewModel.Logout();
        }
    }
}
