using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using MessageBox = System.Windows.MessageBox;

namespace Cinema_WPF.ViewModels
{
    class MainViewViewModel : ViewModelBase
    {
        public MainWindowViewModel ParentMainWindowViewModel { get; set; }
        public ObservableCollection<Film> FilmsCollection { get; private set; }
        public ObservableCollection<Session> Film_SessionsCollection { get; private set; }
        public ObservableCollection<Ticket> SessionTickets { get; private set; }
        public ObservableCollection<Session> SessionCollection { get; private set; }
        public ObservableCollection<Director> DirectorCollection { get; private set; }
        //For add/change Film
        public ObservableCollection<Director> CurrentDirectors { get; private set; }
        public ObservableCollection<Director> Directors { get; private set; }
        public ObservableCollection<Genre> CurrentGenres { get; private set; }
        public ObservableCollection<Genre> Genres { get; private set; }
        //For add/change Director/Session
        public ObservableCollection<Film> CurrentFilms { get; private set; }
        public ObservableCollection<Film> Films { get; private set; }
        public ObservableCollection<Hall> Halls { get; private set; }


        public Visibility FilmsVisibility { get; set; }
        public Visibility SessionsVisibility { get; set; }
        public Visibility SessionVisibility { get; set; }
        public Visibility FilmVisibility { get; set; }
        public Visibility PagingVisibility { get; set; }
        public Visibility BackToFilmVisibility { get; private set; }
        public Visibility BackToSessionsVisibility { get; private set; }
        public Visibility AdminViewVisibility { get; private set; }
        public Visibility AdminFormVisibility { get; private set; }
        public Visibility AdminDerectorsVisibility { get; private set; }
        public Visibility IncorrectYearVisibility { get; private set; }
        public Visibility ChangeButtonVisibility { get; private set; }
        public Visibility AddButtonVisibility { get; private set; }
        public Visibility CalendarErrorVisibility { get; private set; }
        public Visibility FilmChangesVisibility { get; private set; }
        public Visibility DirectorChangesVisibility { get; private set; }
        public Visibility SessionChangesVisibility { get; private set; }





        public CinemaContext db;
        public double SelectedPrice { get; private set; } = 0;
        public List<Ticket> SelectedTickets { get; private set; }
        //public List<Ticket> SessionTickets { get; private set; }
        public User User { get; set; }
        public Film SelectedFilm { get; private set; }
        public Session SelectedSession { get; private set; }
        public Director SelectedDirector { get; private set; }

        //Add or change Film parameters
        public string Film_Year { get; set; }
        public string Film_Name { get; set; }
        public string Film_Description { get; set; }

        //Add or change Director parameters
        public string Director_Year { get; set; }
        public string Director_Mounth { get; set; }
        public string Director_Day { get; set; }
        public string Director_Name { get; set; }
        public string Director_Surname { get; set; }

        //Add or change Session parameters
        public string Session_Year { get; set; }
        public string Session_Mounth { get; set; }
        public string Session_Day { get; set; }
        public string Session_Hour { get; set; }
        public string Session_Minute { get; set; }
        public string Session_Price { get; set; }
        public Hall Session_Hall { get; set; }
        public Film Session_Film { get; set; }

        public int FilmPageCount { get; set; }
        public int Film_SessionPageCount { get; set; }
        public int SessionPageCount { get; set; }
        public int DirectorPageCount { get; set; }
        public int FilmPageSize { get; set; } = 5;
        public int DirectorPageSize { get; set; } = 5;
        public int Film_SessionPageSize { get; set; } = 10;
        public int SessionPageSize { get; set; } = 10;
        public int CurrentFilmsPosition { get; set; } = 1;
        public int CurrentDirectorPosition { get; set; } = 1;
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
        public ICommand AdminViewCommand { get; private set; }
        public ICommand ShowAdminSessionsCommand { get; private set; }
        public ICommand ShowAdminFilmsCommand { get; private set; }
        public ICommand ShowAdminDerectorsCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand RemoveConfirmCommand { get; private set; }

        public ICommand AddFilmCommand { get; private set; }
        public ICommand ChangeFilmCommand { get; private set; }

        public ICommand AddDirectorCommand { get; private set; }
        public ICommand ChangeDirectorCommand { get; private set; }

        public ICommand AddSessionCommand { get; private set; }
        public ICommand ChangeSessionCommand { get; private set; }

        public MainViewViewModel()
        {
            db = new CinemaContext();
            LoadWindow();
            Film_SessionsCollection = null;
            FilmsCollection = null;
            FilmPageCount = GetList.GetFilmPages(db.Films, FilmPageSize);
            SelectedTickets = null;
            ChangeFilmsCollection();
        }

        public void LoadWindow()
        {
            LogoutCommand = new RelayCommand(Logout);
            NextPageCommand = new RelayCommand(NextPage);
            PreviusPageCommand = new RelayCommand(PreviusPage);
            FirstPageCommand = new RelayCommand(FirstPage);
            LastPageCommand = new RelayCommand(LastPage);
            ShowFilmsCommand = new RelayCommand(ShowFilms);
            SellCommand = new RelayCommand(SellTickets);
            ShowFilmCommand = new RelayCommand<Film>(ShowFilm);
            ShowSessionsCommand = new RelayCommand(ShowSessions);
            AdminViewCommand = new RelayCommand(ShowAdminView);
            ShowAdminSessionsCommand = new RelayCommand(ShowAdminSessions);
            ShowAdminFilmsCommand = new RelayCommand(ShowAdminFilms);
            ShowAdminDerectorsCommand = new RelayCommand(ShowAdminDirectors);
            RemoveConfirmCommand = new RelayCommand(Remove);
            AddCommand = new RelayCommand(Add);
            ChangeCommand = new RelayCommand(Change);
            AddDirectorCommand = new RelayCommand(Change);

            AddFilmCommand = new RelayCommand(AddFilm);
            ChangeFilmCommand = new RelayCommand(ChangeFilm);
            AddDirectorCommand = new RelayCommand(AddDirector);
            ChangeDirectorCommand = new RelayCommand(ChangeDirector);
            AddSessionCommand = new RelayCommand(AddSession);
            ChangeSessionCommand = new RelayCommand(ChangeSession);

            IncorrectYearVisibility = Visibility.Hidden;
            FilmsVisibility = Visibility.Visible;
            PagingVisibility = Visibility.Visible;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;
            AdminDerectorsVisibility = Visibility.Hidden;
            BackToSessionsVisibility = Visibility.Hidden;
            AdminFormVisibility = Visibility.Hidden;
            FilmChangesVisibility = Visibility.Hidden;
            DirectorChangesVisibility = Visibility.Hidden;
            SessionChangesVisibility = Visibility.Hidden;
            ChangeButtonVisibility = Visibility.Hidden;
            AddButtonVisibility = Visibility.Hidden;
            CalendarErrorVisibility = Visibility.Hidden;

            if (User != null)
            {
                int id = db.UserRoles.FirstOrDefault(i => i.Name.ToLower() == "admin").Id;
                if (User.UserRoleId == id)
                {
                    
                AdminViewVisibility = Visibility.Visible;
                RaisePropertyChanged(() => AdminViewVisibility);
                }
                else
                {
                    AdminViewVisibility = Visibility.Hidden;
                    RaisePropertyChanged(() => AdminViewVisibility);
                }
            }
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
            if (AdminDerectorsVisibility == Visibility.Visible)
            {
                if (CurrentDirectorPosition != DirectorPageCount)
                    CurrentDirectorPosition++;
                Change_DirectorCollection();
            }
        }

        private void PreviusPage()
        {
            if (FilmsVisibility == Visibility.Visible)
            {
                if (CurrentFilmsPosition != 1)
                    CurrentFilmsPosition--;
                ChangeFilmsCollection();
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
            if (AdminDerectorsVisibility == Visibility.Visible)
            {
                if (CurrentDirectorPosition != 1)
                    CurrentDirectorPosition--;
                Change_DirectorCollection();
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
            if (AdminDerectorsVisibility == Visibility.Visible)
            {
                if (CurrentDirectorPosition != 1)
                    CurrentDirectorPosition = 1;
                Change_DirectorCollection();
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
            if (AdminDerectorsVisibility == Visibility.Visible)
            {
                if (CurrentDirectorPosition != DirectorPageCount)
                    CurrentDirectorPosition = DirectorPageCount;
                Change_DirectorCollection();
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
        private void Change_DirectorCollection()
        {
            CurrentPagePosition = CurrentDirectorPosition;
            PageCount = DirectorPageCount;
            DirectorCollection = GetList.GetDirectors(db.Directors, (CurrentDirectorPosition - 1) * DirectorPageSize, DirectorPageSize);
            RaisePropertyChanged(() => DirectorCollection);
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

        public void Close()
        {
            ParentMainWindowViewModel.Close();
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
            AdminFormVisibility = Visibility.Hidden;
            AdminDerectorsVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;
            FilmChangesVisibility = Visibility.Hidden;
            DirectorChangesVisibility = Visibility.Hidden;
            SessionChangesVisibility = Visibility.Hidden;
            ChangeFilm_SessionCollection();
            RaisePropertyChanged(() => SelectedFilm);
            VisibilityPropertyChanged();
        }

        private void ShowSessions()
        {
            BackToSessionsVisibility = Visibility.Visible;
            AdminDerectorsVisibility = Visibility.Hidden;
            BackToFilmVisibility = Visibility.Hidden;
            FilmsVisibility = Visibility.Hidden;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Visible;
            FilmChangesVisibility = Visibility.Hidden;
            AdminFormVisibility = Visibility.Hidden;
            DirectorChangesVisibility = Visibility.Hidden;
            SessionChangesVisibility = Visibility.Hidden;
            SessionPageCount = GetList.GetSessionPages(new List<Session>(db.Sessions), SessionPageSize);
            CurrentSessionPosition = 1;
            Change_SessionCollection();
            VisibilityPropertyChanged();
            //SessionCollection

        }

        private void ShowAdminView()
        {
            BackToSessionsVisibility = Visibility.Hidden;
            AdminDerectorsVisibility = Visibility.Hidden;
            BackToFilmVisibility = Visibility.Hidden;
            FilmsVisibility = Visibility.Hidden;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;
            AdminFormVisibility = Visibility.Visible;
            FilmChangesVisibility = Visibility.Hidden;
            DirectorChangesVisibility = Visibility.Hidden;
            SessionChangesVisibility = Visibility.Hidden;
            VisibilityPropertyChanged();
        }
        public void ShowFilms()
        {
            CurrentFilm_SessionPosition = 1;
            FilmsVisibility = Visibility.Visible;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            AdminFormVisibility = Visibility.Hidden;
            AdminDerectorsVisibility = Visibility.Hidden;
            FilmChangesVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;
            DirectorChangesVisibility = Visibility.Hidden;
            SessionChangesVisibility = Visibility.Hidden;
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
            RaisePropertyChanged(() => AdminFormVisibility);
            RaisePropertyChanged(() => DirectorChangesVisibility);
            RaisePropertyChanged(() => SessionChangesVisibility);
            RaisePropertyChanged(() => FilmChangesVisibility);
            RaisePropertyChanged(() => AdminDerectorsVisibility);
            RaisePropertyChanged(() => AddButtonVisibility);
            RaisePropertyChanged(() => ChangeButtonVisibility);
        }


        public void ShowSession(Session session)
        {
            FilmsVisibility = Visibility.Hidden;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Visible;
            AdminDerectorsVisibility = Visibility.Hidden;
            AdminFormVisibility = Visibility.Hidden;
            FilmChangesVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;
            DirectorChangesVisibility = Visibility.Hidden;
            SessionChangesVisibility = Visibility.Hidden;
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

        public void ShowAdminSessions()
        {
            CurrentSessionPosition = 1;
            BacktoAdminView();
            BackToFilmVisibility = Visibility.Hidden;
            FilmsVisibility = Visibility.Hidden;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            AdminDerectorsVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Visible;
            FilmChangesVisibility = Visibility.Hidden;
            AdminFormVisibility = Visibility.Visible;
            DirectorChangesVisibility = Visibility.Hidden;
            SessionChangesVisibility = Visibility.Hidden;
            SessionPageCount = GetList.GetSessionPages(new List<Session>(db.Sessions), SessionPageSize);
            Change_SessionCollection();
            VisibilityPropertyChanged();
        }
        private void ShowAdminFilms()
        {
            CurrentFilm_SessionPosition = 1;
            BacktoAdminView();
            FilmsVisibility = Visibility.Visible;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;
            AdminDerectorsVisibility = Visibility.Hidden;
            FilmChangesVisibility = Visibility.Hidden;
            DirectorChangesVisibility = Visibility.Hidden;
            SessionChangesVisibility = Visibility.Hidden;
            AdminFormVisibility = Visibility.Visible;
            ChangeFilmsCollection();
            //RaisePropertyChanged(() => SelectedFilm);
            VisibilityPropertyChanged();
        }

        private void ShowAdminDirectors()
        {
            CurrentDirectorPosition = 1;
            BacktoAdminView();
            DirectorChangesVisibility = Visibility.Hidden;
            FilmChangesVisibility = Visibility.Hidden;
            FilmsVisibility = Visibility.Hidden;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;
            AdminDerectorsVisibility = Visibility.Visible;
            FilmChangesVisibility = Visibility.Hidden;
            DirectorChangesVisibility = Visibility.Hidden;
            SessionChangesVisibility = Visibility.Hidden;
            AdminFormVisibility = Visibility.Visible;
            DirectorPageCount = GetList.GetDirectorPages(db.Directors, DirectorPageSize);
            Change_DirectorCollection();
            //RaisePropertyChanged(() => SelectedFilm);
            VisibilityPropertyChanged();
        }

        private void Change()
        {
            if (FilmsVisibility == Visibility.Visible)
            {
                if (SelectedFilm == null)
                {
                    MessageBox.Show("Select film firstly please!");
                    return;
                }
                CurrentDirectors = new ObservableCollection<Director>();
                Directors = new ObservableCollection<Director>();
                CurrentGenres = new ObservableCollection<Genre>();
                Genres = new ObservableCollection<Genre>();
                GetList.GetDirectorsforFilmChanging(new ObservableCollection<Director>(db.Directors), SelectedFilm, CurrentDirectors, Directors);
                GetList.GetGenresforFilmChanging(new ObservableCollection<Genre>(db.Genres), SelectedFilm,
                    CurrentGenres, Genres);
                AddFilmPropertyChanged();

                Film_Name = SelectedFilm.Name;
                Film_Year = SelectedFilm.Year.ToString();
                Film_Description = SelectedFilm.Desription;

                RaisePropertyChanged(() => Film_Name);
                RaisePropertyChanged(() => Film_Year);
                RaisePropertyChanged(() => Film_Description);

                ChangeButtonVisibility = Visibility.Visible;
                AddButtonVisibility = Visibility.Hidden;
                FilmsVisibility = Visibility.Hidden;
                FilmVisibility = Visibility.Hidden;
                SessionVisibility = Visibility.Hidden;
                SessionsVisibility = Visibility.Hidden;
                AdminDerectorsVisibility = Visibility.Hidden;
                FilmChangesVisibility = Visibility.Visible;
                DirectorChangesVisibility = Visibility.Hidden;
                SessionChangesVisibility = Visibility.Hidden;
                AdminFormVisibility = Visibility.Visible;
                IncorrectYearVisibility = Visibility.Hidden;
                RaisePropertyChanged(() => IncorrectYearVisibility);
            }
            if (AdminDerectorsVisibility == Visibility.Visible)
            {
                if (SelectedDirector == null)
                {
                    MessageBox.Show("Select director firstly please!");
                    return;
                }

                CurrentFilms = new ObservableCollection<Film>();
                Films = new ObservableCollection<Film>();
                GetList.GetFilmsFromDirector(new ObservableCollection<Film>(db.Films), SelectedDirector, CurrentFilms,
                    Films);
                AddDirectorPropertyChanged();
                Director_Name = SelectedDirector.Name;
                Director_Surname = SelectedDirector.Surname;
                Director_Year = SelectedDirector.DateOfBirth.Year.ToString();
                Director_Mounth = SelectedDirector.DateOfBirth.Month.ToString();
                Director_Day = SelectedDirector.DateOfBirth.Day.ToString();


                RaisePropertyChanged(()=> Director_Name);
                RaisePropertyChanged(()=> Director_Surname);
                RaisePropertyChanged(()=> Director_Year);
                RaisePropertyChanged(()=> Director_Mounth);
                RaisePropertyChanged(()=> Director_Day);

                CurrentDirectorPosition = 1;
                ChangeButtonVisibility = Visibility.Visible;
                AddButtonVisibility = Visibility.Hidden;
                FilmsVisibility = Visibility.Hidden;
                FilmVisibility = Visibility.Hidden;
                SessionVisibility = Visibility.Hidden;
                SessionsVisibility = Visibility.Hidden;
                AdminDerectorsVisibility = Visibility.Hidden;
                FilmChangesVisibility = Visibility.Hidden;
                DirectorChangesVisibility = Visibility.Visible;
                SessionChangesVisibility = Visibility.Hidden;
                AdminFormVisibility = Visibility.Visible;
            }
            if (SessionsVisibility == Visibility.Visible)
            {
                var session = SelectedSession;

                Films = new ObservableCollection<Film>(db.Films);
                Halls = new ObservableCollection<Hall>(db.Halls);
                Session_Film = session.Film;
                Session_Hall = session.Hall;
                Session_Price = session.Price.ToString();
                Session_Day = session.DateTime.Day.ToString();
                Session_Year = session.DateTime.Year.ToString();
                Session_Mounth = session.DateTime.Month.ToString();
                Session_Hour = session.DateTime.Hour.ToString();
                Session_Minute = session.DateTime.Minute.ToString();
                SessionChangePropertyChange();

                CurrentDirectorPosition = 1;
                ChangeButtonVisibility = Visibility.Visible;
                AddButtonVisibility = Visibility.Hidden;
                FilmsVisibility = Visibility.Hidden;
                FilmVisibility = Visibility.Hidden;
                SessionVisibility = Visibility.Hidden;
                SessionsVisibility = Visibility.Hidden;
                AdminDerectorsVisibility = Visibility.Hidden;
                FilmChangesVisibility = Visibility.Hidden;
                DirectorChangesVisibility = Visibility.Hidden;
                SessionChangesVisibility = Visibility.Visible;
                AdminFormVisibility = Visibility.Visible;
            }
            VisibilityPropertyChanged();
        }

        private void SessionChangePropertyChange()
        {
            RaisePropertyChanged(() => Session_Film);
            RaisePropertyChanged(() => Session_Hall);
            RaisePropertyChanged(() => Session_Price);
            RaisePropertyChanged(() => Session_Day);
            RaisePropertyChanged(() => Session_Year);
            RaisePropertyChanged(() => Session_Mounth);
            RaisePropertyChanged(() => Session_Hour);
            RaisePropertyChanged(() => Session_Minute);
            RaisePropertyChanged(() => Halls);
            RaisePropertyChanged(() => Films);
        }

        public void Add()
        {
            if (FilmsVisibility == Visibility.Visible)
            {
                CurrentDirectors = null;
                CurrentGenres = null;
                Directors = new ObservableCollection<Director>(db.Directors);
                Genres = new ObservableCollection<Genre>(db.Genres);
                AddFilmPropertyChanged();
                CurrentDirectorPosition = 1;

                ChangeButtonVisibility = Visibility.Hidden;
                AddButtonVisibility = Visibility.Visible;
                FilmsVisibility = Visibility.Hidden;
                FilmVisibility = Visibility.Hidden;
                SessionVisibility = Visibility.Hidden;
                SessionsVisibility = Visibility.Hidden;
                AdminDerectorsVisibility = Visibility.Hidden;

                FilmChangesVisibility = Visibility.Visible;
                DirectorChangesVisibility = Visibility.Hidden;
                SessionChangesVisibility = Visibility.Hidden;
                AdminFormVisibility = Visibility.Visible;
                IncorrectYearVisibility = Visibility.Hidden;
                RaisePropertyChanged(()=> IncorrectYearVisibility);
            }
            if (AdminDerectorsVisibility == Visibility.Visible)
            {
                CurrentFilms = null;
                Films = new ObservableCollection<Film>(db.Films);
                AddDirectorPropertyChanged();
                ChangeButtonVisibility = Visibility.Hidden;
                AddButtonVisibility = Visibility.Visible;
                FilmsVisibility = Visibility.Hidden;
                FilmVisibility = Visibility.Hidden;
                SessionVisibility = Visibility.Hidden;
                SessionsVisibility = Visibility.Hidden;
                AdminDerectorsVisibility = Visibility.Hidden;

                FilmChangesVisibility = Visibility.Hidden;
                DirectorChangesVisibility = Visibility.Visible;
                SessionChangesVisibility = Visibility.Hidden;
                AdminFormVisibility = Visibility.Visible;
                CalendarErrorVisibility =Visibility.Hidden;
                RaisePropertyChanged(()=> CalendarErrorVisibility);
            }
            if (SessionsVisibility == Visibility.Visible)
            {
                Films = new ObservableCollection<Film>(db.Films);
                Halls = new ObservableCollection<Hall>(db.Halls);
                RaisePropertyChanged(()=> Films);
                RaisePropertyChanged(()=> Halls);
                SessionChangePropertyChange();
                ChangeButtonVisibility = Visibility.Hidden;
                AddButtonVisibility = Visibility.Visible;
                FilmsVisibility = Visibility.Hidden;
                FilmVisibility = Visibility.Hidden;
                SessionVisibility = Visibility.Hidden;
                SessionsVisibility = Visibility.Hidden;
                AdminDerectorsVisibility = Visibility.Hidden;
                FilmChangesVisibility = Visibility.Hidden;
                DirectorChangesVisibility = Visibility.Hidden;
                SessionChangesVisibility = Visibility.Visible;
                AdminFormVisibility = Visibility.Visible;
            }
            VisibilityPropertyChanged();
        }

        public void BacktoAdminView()
        {
            CurrentDirectors = null;
            CurrentGenres = null;
            Directors = null;
            Genres = null;
            Director_Name = null;
            Director_Surname = null;
            Director_Year = null;
            Director_Mounth = null;
            Director_Day = null;
            Session_Year = null;
            Session_Mounth = null;
            Session_Day = null;
            Session_Hour = null;
            Session_Minute = null;
            Session_Price = null;
            Session_Hall = null;
            Session_Film = null;

            SessionChangePropertyChange();
            RaisePropertyChanged(()=> Halls);
            AddDirectorPropertyChanged();
            AddFilmPropertyChanged();
        }

        public void Remove()
        {
            if (FilmsVisibility == Visibility.Visible)
            {
                if (SelectedFilm != null)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Delete " + SelectedFilm.Name + "?\n If you delete this film - Sessions with this film will be deleted also!!!", "Delete Confirmation", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        var film = db.Films.FirstOrDefault(f => f.Id == SelectedFilm.Id);
                        if (film != null)
                        {
                            film.ClearList();
                            db.Films.Remove(film);
                            db.SaveChanges();
                        }

                        ShowAdminFilms();
                    }
                    else 
                        return;
                }
            }
            if (AdminDerectorsVisibility == Visibility.Visible)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Delete " + SelectedDirector.Name + " " + SelectedDirector.Surname + "?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var director = db.Directors.FirstOrDefault(f => f.Id == SelectedDirector.Id);
                    if (director != null)
                    {
                        director.Films.Clear();
                        db.Directors.Remove(director);
                        db.SaveChanges();
                    }
                    ShowAdminDirectors();
                }
            }
            if (SessionsVisibility == Visibility.Visible)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Delete this session ?", "Delete Confirmation", MessageBoxButton.YesNo);

            }
        }

        private void AddFilmPropertyChanged()
        {
            RaisePropertyChanged(() => CurrentDirectors);
            RaisePropertyChanged(() => Directors);
            RaisePropertyChanged(() => Genres);
            RaisePropertyChanged(() => CurrentGenres);

        }

        private void AddDirectorPropertyChanged()
        {
            RaisePropertyChanged(() => CurrentFilms);
            RaisePropertyChanged(() => Films);
        }


        public void AddDirectorToFilm(Director director)
        {

            CurrentDirectors = GetList.AddDirector(CurrentDirectors, director);
            Directors = GetList.RemoveDirector(Directors, director);
            AddFilmPropertyChanged();
        }

        public void RemoveDirectorFromFilm(Director director)
        {

            CurrentDirectors = GetList.RemoveDirector(CurrentDirectors, director);
            Directors = GetList.AddDirector(Directors, director);
            AddFilmPropertyChanged();
        }
        public void AddGenreToFilm(Genre genre)
        {

            CurrentGenres = GetList.AddGenre(CurrentGenres, genre);
            Genres = GetList.RemoveGenre(Genres, genre);
            AddFilmPropertyChanged();
        }

        public void RemoveGenreFromFilm(Genre genre)
        {

            CurrentGenres = GetList.RemoveGenre(CurrentGenres, genre);
            Genres = GetList.AddGenre(Genres, genre);
            AddFilmPropertyChanged();
        }

        public void AddFilmToDirector(Film film)
        {
            CurrentFilms = GetList.AddFilm(CurrentFilms, film);
            Films = GetList.RemoveFilm(Films, film);
            AddDirectorPropertyChanged();
        }

        public void RemoveFilmFromDirector(Film film)
        {

            CurrentFilms = GetList.RemoveFilm(CurrentFilms, film);
            Films = GetList.AddFilm(Films, film);
            AddDirectorPropertyChanged();
        }

        public void SelectFilmInSession(Film film)
        {
            Session_Film = film;
            RaisePropertyChanged(()=>Session_Film);
        }

        public void SelectHallInSession(Hall hall)
        {
            Session_Hall = hall;
            RaisePropertyChanged(() => Session_Hall);
        }

        public void Select_Film(Film film)
        {
            SelectedFilm = film;
        }

        public void Select_Session(Session session)
        {
            SelectedSession = session;
        }

        public void Select_Director(Director director)
        {
            SelectedDirector = director;
        }

        private void AddFilm()
        {
            int year = Int32.Parse(Film_Year);
            if (year < 1900 || year > DateTime.Now.Year)
            {
                IncorrectYearVisibility = Visibility.Visible;
                RaisePropertyChanged(() => IncorrectYearVisibility);
                return;
            }
            else
            {
                IncorrectYearVisibility = Visibility.Hidden;
                RaisePropertyChanged(() => IncorrectYearVisibility);
            }

            Film film = new Film();
            film.Name = Film_Name;
            film.Year = year;
            film.Desription = Film_Description;

            for (int i = 0; i < CurrentDirectors.Count; i++)
            {
                film.Directors.Add(CurrentDirectors[i]);
            }

            for (int i = 0; i < CurrentGenres.Count; i++)
            {
                film.Genres.Add(CurrentGenres[i]);
            }

            db.Films.Add(film);
            db.SaveChanges();
            ShowAdminFilms();
        }

        private void ChangeFilm()
        {
            int year = Int32.Parse(Film_Year);
            if (year < 1900 || year > DateTime.Now.Year)
            {
                IncorrectYearVisibility = Visibility.Visible;
                RaisePropertyChanged(() => IncorrectYearVisibility);
                return;
            }
            else
            {
                IncorrectYearVisibility = Visibility.Hidden;
                RaisePropertyChanged(() => IncorrectYearVisibility);
            }

            Film film = db.Films.Find(SelectedFilm.Id);
            film.Name = Film_Name;
            film.Year = year;
            film.Desription = Film_Description;

            film.Directors = new List<Director>();
            film.Genres = new List<Genre>();

            for (int i = 0; i < CurrentDirectors.Count; i++)
            {
                film.Directors.Add(CurrentDirectors[i]);
            }

            for (int i = 0; i < CurrentGenres.Count; i++)
            {
                film.Genres.Add(CurrentGenres[i]);
            }

            db.Entry(film).State = EntityState.Modified;
            db.SaveChanges();
            ShowAdminFilms();
        }

        private void AddDirector()
        {
            int day = Int32.Parse(Director_Day);
            int mounth = Int32.Parse(Director_Mounth);
            int year = Int32.Parse(Director_Year);

            if ((day > 31 || day < 1) || (mounth > 12 || mounth < 1) || (year > DateTime.Now.Year || year < 1890))
            {
                CalendarErrorVisibility = Visibility.Visible;
                RaisePropertyChanged(()=>CalendarErrorVisibility);
                return;
            }
            else
            {
                CalendarErrorVisibility = Visibility.Hidden;
                RaisePropertyChanged(() => CalendarErrorVisibility);
            }

            Director director = new Director();
            director.Name = Director_Name;
            director.Surname = Director_Surname;
            director.DateOfBirth = new DateTime(year, mounth, day);

            for (int i = 0; i < CurrentFilms.Count; i++)
            {
                director.Films.Add(CurrentFilms[i]);
            }

            db.Directors.Add(director);
            db.SaveChanges();
            ShowAdminDirectors();
            ShowAdminDirectors();

        }

        private void ChangeDirector()
        {
            int day = Int32.Parse(Director_Day);
            int mounth = Int32.Parse(Director_Mounth);
            int year = Int32.Parse(Director_Year);

            if ((day > 31 || day < 1) || (mounth > 12 || mounth < 1) || (year > DateTime.Now.Year || year < 1890))
            {
                CalendarErrorVisibility = Visibility.Visible;
                RaisePropertyChanged(() => CalendarErrorVisibility);
                return;
            }
            else
            {
                CalendarErrorVisibility = Visibility.Hidden;
                RaisePropertyChanged(() => CalendarErrorVisibility);
            }

            Director director = db.Directors.Find(SelectedDirector.Id);
            director.Name = Director_Name;
            director.Surname = Director_Surname;
            director.DateOfBirth = new DateTime(year, mounth, day);
            director.Films = new List<Film>();
            for (int i = 0; i < CurrentFilms.Count; i++)
            {
                director.Films.Add(CurrentFilms[i]);
            }

            db.Entry(director).State = EntityState.Modified;
            db.SaveChanges();
            ShowAdminDirectors();

        }

        private void AddSession()
        {
            int day = Int32.Parse(Session_Day);
            int mounth = Int32.Parse(Session_Mounth);
            int year = Int32.Parse(Session_Year);
            int hour = Int32.Parse(Session_Hour);
            int minute = Int32.Parse(Session_Minute);

            if ((day > 31 || day < 1) || (mounth > 12 || mounth < 1) || (year > DateTime.Now.Year || year < 1890) || (hour >= 24 || hour < 0) || (minute >=60 || minute < 0))
            {
                CalendarErrorVisibility = Visibility.Visible;
                RaisePropertyChanged(() => CalendarErrorVisibility);
                return;
            }
            else
            {
                CalendarErrorVisibility = Visibility.Hidden;
                RaisePropertyChanged(() => CalendarErrorVisibility);
            }

            Session session = new Session();
            session.Film = Session_Film;
            session.Hall = Session_Hall;
            session.Price = Double.Parse(Session_Price);
            session.DateTime = new DateTime(year, mounth, day, hour, minute, 0);

            db.Sessions.Add(session);
            db.SaveChanges();

            ShowAdminDirectors();
        }

        private void ChangeSession()
        {
            int day = Int32.Parse(Session_Day);
            int mounth = Int32.Parse(Session_Mounth);
            int year = Int32.Parse(Session_Year);
            int hour = Int32.Parse(Session_Hour);
            int minute = Int32.Parse(Session_Minute);

            if ((day > 31 || day < 1) || (mounth > 12 || mounth < 1) || (year > DateTime.Now.Year || year < 1890) || (hour >= 24 || hour < 0) || (minute >= 60 || minute < 0))
            {
                CalendarErrorVisibility = Visibility.Visible;
                RaisePropertyChanged(() => CalendarErrorVisibility);
                return;
            }
            else
            {
                CalendarErrorVisibility = Visibility.Hidden;
                RaisePropertyChanged(() => CalendarErrorVisibility);
            }

            Session session = db.Sessions.Find(SelectedSession.Id);
            session.Film = Session_Film;
            session.Hall = Session_Hall;
            session.Price = Double.Parse(Session_Price);
            session.DateTime = new DateTime(year, mounth, day, hour, minute, 0);

            db.Entry(session).State = EntityState.Modified;
            db.SaveChanges();
            ShowAdminSessions();
        }



    }
}
