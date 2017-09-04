using System;
using System.Collections.Generic;
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
using Cinema_WPF.Models;
using Cinema_WPF.ViewModels;

namespace Cinema_WPF.Views
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        public AdminView()
        {
            InitializeComponent();
        }

        private void SelectFilm_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;
            Film film = (sender as ListViewItem).Content as Film;

            datacontext.Select_Film(film);
        }

        private void SelectSession_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;
            Session session = (sender as ListViewItem).Content as Session;

            datacontext.Select_Session(session);
        }

        private void SelectDirector_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;
            Director director = (sender as ListViewItem).Content as Director;

            datacontext.Select_Director(director);
        }
    }
}
