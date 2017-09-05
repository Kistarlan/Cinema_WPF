using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddDirectorToFilm_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;

            Director director = (sender as ComboBoxItem).Content as Director;

            datacontext.AddDirectorToFilm(director);
        }

        private void RemoveDirectorFromFilm_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;

            Director director = (sender as ListBoxItem).Content as Director;

            datacontext.RemoveDirectorFromFilm(director);
        }

        private void AddGenreToFilm_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;

            Genre genre = (sender as ComboBoxItem).Content as Genre;

            datacontext.AddGenreToFilm(genre);
        }

        private void RemoveGenreFromFilm_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;

            Genre genre = (sender as ListBoxItem).Content as Genre;

            datacontext.RemoveGenreFromFilm(genre);
        }

        private void AddFilmToDirector_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;

            Film film = (sender as ComboBoxItem).Content as Film;

            datacontext.AddFilmToDirector(film);
        }

        private void RemoveFilmFromDirector_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;

            Film film = (sender as ListBoxItem).Content as Film;

            datacontext.RemoveFilmFromDirector(film);
        }

        private void SelectFilmToSession_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;

            Film film = (sender as ComboBoxItem).Content as Film;

            datacontext.SelectFilmInSession(film);
        }

        private void SelectHallToSession_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var datacontext = this.DataContext as MainViewViewModel;

            Hall hall = (sender as ComboBoxItem).Content as Hall;

            datacontext.SelectHallInSession(hall);
        }
    }
}
