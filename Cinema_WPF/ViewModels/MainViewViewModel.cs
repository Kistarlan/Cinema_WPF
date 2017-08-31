using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using Cinema_WPF.Context;
using Cinema_WPF.Helper;
using Cinema_WPF.Models;
using Cinema_WPF.Views;

namespace Cinema_WPF.ViewModels
{
    class MainViewViewModel : ViewModelBase
    {
        public MainWindowViewModel ParentMainWindowViewModel { get; set; }
        public ObservableCollection<Film> FilmsCollection { get; private set; }
        public ObservableCollection<Session> Film_SessionsCollection { get; private set; }
        public ObservableCollection<Ticket> SessionTickets { get; private set; }
        public ObservableCollection<Session> SessionCollection { get; private set; }

        public Visibility FilmsVisibility { get; set; }
        public Visibility SessionsVisibility { get; set; }
        public Visibility SessionVisibility { get; set; }
        public Visibility FilmVisibility { get; set; }
        public Visibility PagingVisibility { get; set; }
        public Visibility BackToFilmVisibility { get; private set; }
        public Visibility BackToSessionsVisibility { get; private set; }



        public CinemaContext db;
        public double SelectedPrice { get; private set; } = 0;
        public List<Ticket> SelectedTickets { get; private set; }
        //public List<Ticket> SessionTickets { get; private set; }
        public User User { get; set; }
        public Film SelectedFilm { get; private set; }
        public Session SelectedSession { get; private set; }
        public int FilmPageCount { get; set; }
        public int Film_SessionPageCount { get; set; }
        public int SessionPageCount { get; set; }
        public int FilmPageSize { get; set; } = 5;
        public int Film_SessionPageSize { get; set; } = 10;
        public int SessionPageSize { get; set; } = 10;
        public int CurrentFilmsPosition { get; set; } = 1;
        public int CurrentFilm_SessionPosition { get; set; } = 1;
        public int CurrentSessionPosition { get; set; } = 1;
        public int CurrentPagePosition { get; set; }
        public int PageCount { get; set; }

        public ICommand LogoutCommand { get; private set; }
        public ICommand ShowFilmsCommand { get; private set; }
        public ICommand NextPageCommand { get; private set; }
        public ICommand LastPageCommand { get; private set; }
        public ICommand FirstPageCommand { get; private set; }
        public ICommand PreviusPageCommand { get; private set; }
        public ICommand SellCommand { get; private set; }
        public ICommand ShowFilmCommand { get; private set; }
        public ICommand ShowSessionsCommand { get; private set; }
        


        public MainViewViewModel()
        {
            db = new CinemaContext();
            LogoutCommand = new RelayCommand(Logout);
            NextPageCommand = new RelayCommand(NextPage);
            PreviusPageCommand = new RelayCommand(PreviusPage);
            FirstPageCommand = new RelayCommand(FirstPage);
            LastPageCommand = new RelayCommand(LastPage);
            ShowFilmsCommand = new RelayCommand(ShowFilms);
            SellCommand = new RelayCommand(SellTickets);
            ShowFilmCommand = new RelayCommand<Film>(ShowFilm);
            ShowSessionsCommand = new RelayCommand(ShowSessions);
            FilmsVisibility = Visibility.Visible;
            PagingVisibility = Visibility.Visible;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;
            BackToSessionsVisibility = Visibility.Hidden;
            Film_SessionsCollection = null;
            FilmsCollection = null;
            FilmPageCount = GetList.GetFilmPages(db.Films, FilmPageSize);
            SelectedTickets = null;
            ChangeFilmsCollection();
        }

        private void NextPage()
        {
            if (FilmsVisibility == Visibility.Visible)
            {
                if (CurrentFilmsPosition != FilmPageCount)
                    CurrentFilmsPosition++;
                ChangeFilmsCollection();
            }
            if (FilmVisibility == Visibility.Visible)
            {
                if (CurrentFilm_SessionPosition != Film_SessionPageCount)
                    CurrentFilm_SessionPosition++;
                ChangeFilm_SessionCollection();
            }
            if (SessionsVisibility == Visibility.Visible)
            {
                if (CurrentSessionPosition != SessionPageCount)
                    CurrentSessionPosition++;
                Change_SessionCollection();
            }
        }

        private void PreviusPage()
        {
            if (FilmsVisibility == Visibility.Visible)
            {
                if (CurrentFilmsPosition != 1)
                    CurrentFilmsPosition--;
                ChangeFilm_SessionCollection();
            }
            if (FilmVisibility == Visibility.Visible)
            {
                if (CurrentFilm_SessionPosition != 1)
                    CurrentFilm_SessionPosition--;
                ChangeFilm_SessionCollection();
            }
            if (SessionsVisibility == Visibility.Visible)
            {
                if (CurrentSessionPosition != 1)
                    CurrentSessionPosition--;
                Change_SessionCollection();
            }
        }
        private void FirstPage()
        {
            if (FilmsVisibility == Visibility.Visible)
            {
                if (CurrentFilmsPosition != 1)
                    CurrentFilmsPosition = 1;
                ChangeFilmsCollection();
            }
            if (FilmVisibility == Visibility.Visible)
            {
                if (CurrentFilm_SessionPosition != 1)
                    CurrentFilm_SessionPosition = 1;
                ChangeFilm_SessionCollection();
            }
            if (SessionsVisibility == Visibility.Visible)
            {
                if (CurrentSessionPosition != 1)
                    CurrentSessionPosition = 1;
                Change_SessionCollection();
            }
        }
        private void LastPage()
        {
            if (FilmsVisibility == Visibility.Visible)
            {
                if (CurrentFilmsPosition != FilmPageCount)
                    CurrentFilmsPosition = FilmPageCount;
                ChangeFilmsCollection();
            }
            if (FilmVisibility == Visibility.Visible)
            {
                if (CurrentFilm_SessionPosition != Film_SessionPageCount)
                    CurrentFilm_SessionPosition = Film_SessionPageCount;
                ChangeFilm_SessionCollection();
            }
            if (SessionsVisibility == Visibility.Visible)
            {
                if (CurrentSessionPosition != SessionPageCount)
                    CurrentSessionPosition = SessionPageCount;
                Change_SessionCollection();
            }
        }

        private void ChangeFilmsCollection()
        {
            CurrentPagePosition = CurrentFilmsPosition;
            PageCount = FilmPageCount;
            FilmsCollection = GetList.GetFilms(db.Films, (CurrentFilmsPosition - 1) * FilmPageSize, FilmPageSize);
            RaisePropertyChanged(()=> FilmsCollection);
            ChangePaging();
        }

        private void ChangeFilm_SessionCollection()
        {
            CurrentPagePosition = CurrentFilm_SessionPosition;
            PageCount = Film_SessionPageCount;
            Film_SessionsCollection = GetList.Get_Sessions(new List<Session>(db.Sessions.Where(s => s.FilmId == SelectedFilm.Id)), (CurrentFilm_SessionPosition - 1) * Film_SessionPageSize, Film_SessionPageSize);
            RaisePropertyChanged(() => Film_SessionsCollection);
            ChangePaging();
        }

        private void Change_SessionCollection()
        {
            CurrentPagePosition = CurrentSessionPosition;
            PageCount = SessionPageCount;
            SessionCollection = GetList.Get_Sessions(new List<Session>(db.Sessions), (CurrentSessionPosition - 1) * SessionPageSize, SessionPageSize);
            RaisePropertyChanged(() => SessionCollection);
            ChangePaging();
        }

        private void ChangePaging()
        {
            RaisePropertyChanged(() => CurrentPagePosition);
            RaisePropertyChanged(() => PageCount);
        }

        private void Logout()
        {
            ParentMainWindowViewModel.Logout();
        }

        public void Show()
        {
            RaisePropertyChanged("User");
        }

        public void ShowFilm(Film film)
        {
            BackToFilmVisibility = Visibility.Visible;
            BackToSessionsVisibility = Visibility.Hidden;

            SelectedFilm = film;
            var ss = new List<Session>(db.Sessions.Where(s => s.FilmId == SelectedFilm.Id));
            Film_SessionPageCount = GetList.GetSessionPages(
                ss, Film_SessionPageSize);
            CurrentFilm_SessionPosition = 1;
            FilmsVisibility = Visibility.Hidden;
            FilmVisibility = Visibility.Visible;
            SessionVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;
            ChangeFilm_SessionCollection();
            RaisePropertyChanged(() => SelectedFilm);
            VisibilityPropertyChanged();
        }

        private void ShowSessions()
        {
            BackToSessionsVisibility = Visibility.Visible;
            BackToFilmVisibility = Visibility.Hidden;
            FilmsVisibility = Visibility.Hidden;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Visible;
            SessionPageCount = GetList.GetSessionPages(new List<Session>(db.Sessions), SessionPageSize);
            CurrentSessionPosition = 1;
            Change_SessionCollection();
            VisibilityPropertyChanged();
            //SessionCollection

        }
        public void ShowFilms()
        {
            CurrentFilm_SessionPosition = 1;
            FilmsVisibility = Visibility.Visible;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;
            ChangeFilmsCollection();
            //RaisePropertyChanged(() => SelectedFilm);
            VisibilityPropertyChanged();
        }

        private void VisibilityPropertyChanged()
        {
            RaisePropertyChanged(() => FilmsVisibility);
            RaisePropertyChanged(() => FilmVisibility);
            RaisePropertyChanged(() => SessionsVisibility);
            RaisePropertyChanged(() => SessionVisibility);
            RaisePropertyChanged(() => BackToFilmVisibility);
            RaisePropertyChanged(() => BackToSessionsVisibility);
        }


        public void ShowSession(Session session)
        {
            FilmsVisibility = Visibility.Hidden;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Visible;
            SessionsVisibility = Visibility.Hidden;
            SelectedSession = session;
            SessionTickets = GetList.GetTickets(SelectedSession);
            SelectedTickets = new List<Ticket>();
            //Ticket.SetOrders(SelectedSession);
            TicketPropertyChanged();
            VisibilityPropertyChanged();
        }



        public void AddTicketToCart(Ticket ticket)
        {
            //ticket.Ordered = Visibility.Hidden;
            //ticket.Row = 100000000;
            //ticket.Ordered = Visibility.Visible;
            //var t = db.Tickets.FirstOrDefault(s => s.Id == ticket.Id);
            ////t.Exist = false;
            ticket.Ordered = false;
            //db.SaveChanges();
            SessionTickets = GetList.GetTickets(SelectedSession);
            SelectedTickets.Add(ticket);
            SelectedPrice += SelectedSession.Price;
            //RaisePropertyChanged(() => ticket);
            TicketPropertyChanged();
        }

        public void RemoveTicketFromCart(Ticket ticket)
        {
            ticket.Ordered = true;
            SelectedTickets.Remove(ticket);
            SessionTickets = GetList.GetTickets(SelectedSession);
            SelectedPrice -= SelectedSession.Price;
            TicketPropertyChanged();
        }

        private void TicketPropertyChanged()
        {
            RaisePropertyChanged(() => SelectedSession);
            RaisePropertyChanged(() => SelectedPrice);
            RaisePropertyChanged(() => SelectedTickets);
            RaisePropertyChanged(() => SessionTickets);
        }

        private void SellTickets()
        {
            ConfirmSell confirmSell = new ConfirmSell(SelectedTickets, SelectedPrice);
            if(confirmSell.ShowDialog() == false)
                return;
            SessionTickets = null;
            db = new CinemaContext();
            for (int i = 0; i < SelectedTickets.Count; i++)
            {
                var ticket = db.Tickets.Find(SelectedTickets[i].Id);
                if (ticket != null)
                {
                    ticket.Ordered = true;
                    ticket.Exist = false;
                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();
                }
                SelectedTickets[i].Ordered = true;
                SelectedTickets[i].Exist = false;
            }
            db.SaveChanges();
            TicketPropertyChanged();
            SessionTickets = GetList.GetTickets(SelectedSession);
            SelectedPrice = 0;
            SelectedTickets = new List<Ticket>();
            SelectedPrice = 0;
            TicketPropertyChanged();
        }
    }
}
