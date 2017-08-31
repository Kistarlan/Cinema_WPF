using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;
using Cinema_WPF.Models;

namespace Cinema_WPF.Views
{
    /// <summary>
    /// Interaction logic for ConfirmSell.xaml
    /// </summary>
    public partial class ConfirmSell : Window
    {
        public List<Ticket> tickets;
        public ConfirmSell(List<Ticket> ts, double price)
        {
            tickets = ts;
            InitializeComponent();
            PriceTextBlock.Text = price.ToString(CultureInfo.InvariantCulture);
            for (int i = 0; i < tickets.Count; i++)
            {
                TicketList.Items.Add(tickets[i]);
            }
        }

        private void Sell_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
