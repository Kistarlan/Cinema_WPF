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
using System.Windows.Input;
using Cinema_WPF.Context;
using Cinema_WPF.Helper;
using Cinema_WPF.Models;

namespace Cinema_WPF.ViewModels
{
    class MainViewViewModel : ViewModelBase
    {
        public MainWindowViewModel ParentMainWindowViewModel { get; set; }
        public ObservableCollection<Film> FilmsCollection { get; private set; }

        public Visibility FilmsVisibility { get; set; }
        public Visibility SessionsVisibility { get; set; }
        public Visibility SessionVisibility { get; set; }
        public Visibility FilmVisibility { get; set; }



        public CinemaContext db;
        public User User { get; set; }
        public Film SelectedFilm { get; private set; }
        public int FilmPageCount { get; set; }
        public int FilmPageSize { get; set; } = 5;
        public int CurrentFilmsPosition { get; set; } = 1;


        public ICommand LogoutCommand { get; private set; }
        public ICommand ShowFilmsCommand { get; private set; }
        public ICommand NextFilmPageCommand { get; private set; }
        public ICommand LastFilmPageCommand { get; private set; }
        public ICommand FirstFilmPageCommand { get; private set; }
        public ICommand PreviusFilmPageCommand { get; private set; }



        public MainViewViewModel()
        {
            db = new CinemaContext();
            LogoutCommand = new RelayCommand(Logout);
            NextFilmPageCommand = new RelayCommand(NextFilmPage);
            PreviusFilmPageCommand = new RelayCommand(PreviusFilmPage);
            FirstFilmPageCommand = new RelayCommand(FirstFilmPage);
            LastFilmPageCommand = new RelayCommand(LastFilmPage);
            ShowFilmsCommand = new RelayCommand(ShowFilms);

            FilmsVisibility = Visibility.Visible;
            FilmVisibility = Visibility.Hidden;
            SessionVisibility = Visibility.Hidden;
            SessionsVisibility = Visibility.Hidden;

            FilmsCollection = null;
            FilmPageCount = GetList.GetFilmPages(db.Films, FilmPageSize);
            ChangeFilmsCollection();
        }

        private void NextFilmPage()
        {
            if(CurrentFilmsPosition != FilmPageCount)
                CurrentFilmsPosition++;
            ChangeFilmsCollection();
        }

        private void PreviusFilmPage()
        {
            if (CurrentFilmsPosition != 1)
                CurrentFilmsPosition--;
            ChangeFilmsCollection();
        }
        private void FirstFilmPage()
        {
            if (CurrentFilmsPosition != 1)
                CurrentFilmsPosition = 1;
            ChangeFilmsCollection();
        }
        private void LastFilmPage()
        {
            if (CurrentFilmsPosition != FilmPageCount)
                CurrentFilmsPosition = FilmPageCount;
            ChangeFilmsCollection();
        }

        private void ChangeFilmsCollection()
        {
            FilmsCollection = GetList.GetFilms(db.Films, (CurrentFilmsPosition - 1) * FilmPageSize, FilmPageSize);
            RaisePropertyChanged(()=> FilmsCollection);
            RaisePropertyChanged(()=> CurrentFilmsPosition);
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
            SelectedFilm = film;

            FilmsVisibility = Visibility.Hidden;
            FilmVisibility = Visibility.Visible;
            RaisePropertyChanged(() => SelectedFilm);
            VisibilityPropertyChanged();
        }

        public void ShowFilms()
        {

            FilmsVisibility = Visibility.Visible;
            FilmVisibility = Visibility.Hidden;
            //RaisePropertyChanged(() => SelectedFilm);
            VisibilityPropertyChanged();
        }

        private void VisibilityPropertyChanged()
        {
            RaisePropertyChanged(() => FilmsVisibility);
            RaisePropertyChanged(() => FilmVisibility);
            RaisePropertyChanged(() => SessionsVisibility);
            RaisePropertyChanged(() => SessionVisibility);
        }
    }
}
