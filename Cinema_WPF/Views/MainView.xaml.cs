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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseClick(object sender, MouseButtonEventArgs e)
        {
            Film film = (sender as ListViewItem).Content as Film;
            if (film != null)
            {
                (this.DataContext as MainViewViewModel).ShowFilm(film);
            }
        }

        private void ListViewSessionItem_MouseClick(object sender, MouseButtonEventArgs e)
        {
            Session session = (sender as ListViewItem).Content as Session;
            if (session != null)
            {
                (this.DataContext as MainViewViewModel).ShowSession(session);
            }
        }
    }
}
