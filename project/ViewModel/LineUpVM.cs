using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using project.Model;
using project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace project.ViewModel
{
    class LineUpVM:ObservableObject, IPage
    {
        public string Name
        {
            get { return "LineUp"; }
        }
        public LineUpVM() 
        {

            
            GeselecteerdeBand = new Band(); 
           
            _bands = Band.Bands();
          
        
         }

        private DateTime _selectedDag;

        public DateTime SelectedDag
        {
            get { return _selectedDag; }
            set { _selectedDag = value; }
        }
        


        private ObservableCollection<DateTime> _startTijden;

        public ObservableCollection<DateTime> StartTijden
        {
            get {
                if (SelectedDag != null)
                {
                    _startTijden = Tijden();
                    //foreach (LineUp temp in LineUps)
                    //{
                    //    if (temp.Date.Equals(SelectedDag))
                    //    {
                    //        if (_startTijden.Contains(temp.From))
                    //        {

                    //            _startTijden.Remove(temp.From);
                              
                    //        }
                        
                    //    }
                    
                    //}


                }
                return _startTijden; }
            set { _startTijden = value; }
        }
        private ObservableCollection<DateTime> _eindTijden;

        public ObservableCollection<DateTime> EindTijden
        {
            get
            {
                
               _eindTijden = Tijden();
                return _eindTijden;
            }
            set { _eindTijden = value; }
        }

        public ObservableCollection<DateTime> Tijden()
        {
            DateTime uur = new DateTime();
            ObservableCollection<DateTime> startTijd = new ObservableCollection<DateTime>();
           
            return startTijd;
        
        }

        private ObservableCollection<DateTime> _festivalDays;
        public ObservableCollection<DateTime> FestivalDays
        {
            get
            {
                _festivalDays = Festival.HaalDatum().FestivalDays;
                return _festivalDays;
            }
            set
            {
                _festivalDays = value;
                OnPropertyChanged("FestivalDays");
            }
        }


        public ICommand LoadPicture {

           get { return new RelayCommand(GetString); }
            internal set { }
        }

        private ImageSource _picSource;
        public ImageSource PicSource {
            get { return _picSource; }
            set {
                _picSource = value;
                
                
            }
        
        }

        public void GetString()
        {
            OpenFileDialog dp = new OpenFileDialog();
            dp.Filter = "jpeg|*.jpg|png|*.png";
            if (dp.ShowDialog() != false)
            {

                PicSource = (ImageSource)new ImageSourceConverter().ConvertFromString(dp.FileName); ;
              
            }
         
        }

        private Genre _geselecteerdeGenreInAddedGenres;

        public Genre GeselecteerdeGenreInAddedGenres
        {
            get { return _geselecteerdeGenreInAddedGenres; }
            set { _geselecteerdeGenreInAddedGenres = value; OnPropertyChanged("GeselecteerdeGenreInAddedGenres"); }
        }

        public ICommand DeleteGenreCommand
        {
            get { return new RelayCommand(DeleteGenre, CanDeleteGenre); }
        }
        public void DeleteGenre()
        {
            GeselecteerdeBand.Genres.Remove(GeselecteerdeGenreInAddedGenres);
        
        }
        public bool CanDeleteGenre()
        {
            if (GeselecteerdeGenreInAddedGenres != null)
            {

                return true;
            }
            else
            {

                return false;
            }
        
        }
        public ICommand AddGenreCommand
        {
            get { return new RelayCommand<object>(InsertGenre, CanInsertGenre); }
        
        }
        public void InsertGenre(object par)
        {
            if (GeselecteerdeBand.Genres != null)
            {

                GeselecteerdeBand.Genres.Add((Genre)par);
            }
            else { 
                
                GeselecteerdeBand.Genres = new ObservableCollection<Genre>(); 
                GeselecteerdeBand.Genres.Add((Genre)par); 
                OnPropertyChanged("GeselecteerdeBand"); 
            }
        
        }
        public bool CanInsertGenre(object par)
        {
            if (SelectedGenreForBand != null)
            {

                return true;
            }
            else
            {

                return false;
            }
          
        }
        public ICommand AddBandCommand {
            get { return new RelayCommand<object[]>(InsertBand, CanInsert); }
          
        
        
        }
        public void InsertBand(object[] param)
        {
            string pic = "";
            string descrp = "";
            string facebook = "";
              string twitter = "";
            string name = param[0].ToString();
            if (param[1] != null)
           
            {
                 pic = param[1].ToString();
            }
            if (param[2] != null)
            {
                descrp = param[2].ToString();
            }
            if (param[3] != null)
            {
                facebook = param[3].ToString();
            }
            if (param[4] != null)
            {
                twitter = param[4].ToString();
            }
            ObservableCollection<Genre> genres = new ObservableCollection<Genre>();
            genres = (ObservableCollection<Genre>)param[5];
        
            Band.Add(name, pic, descrp, facebook, twitter, genres );
        }
        public bool CanInsert(object[] param)
        {
            if (param != null)
            {
                if (param[0].ToString() != "" && param[5] != null)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            else { return false; }
        
        }


        public ICommand EditBandCommand
        { get { return new RelayCommand(EditBand, CanEditBand); } }
        public bool CanEditBand()
        {
            if (GeselecteerdeBand != null)
            {
                if (GeselecteerdeBand.Name != null && GeselecteerdeBand.Genres != null)
                {
                    return true;

                }
                else { return false; }
            
            }
            else { return false; }
        
        }
        public void EditBand()
        {
            if (GeselecteerdeBand.Picture== null)
            {
                GeselecteerdeBand.Picture = "";
            }
            if (GeselecteerdeBand.Description == null)
            {
                GeselecteerdeBand.Description = "";
            }
            if (GeselecteerdeBand.Facebook == null)
            {
                GeselecteerdeBand.Facebook = "";
            }
            if (GeselecteerdeBand.Twitter == null)
            {
                GeselecteerdeBand.Twitter = "";
            }
            Band.Edit(GeselecteerdeBand);
        
        }


        private ObservableCollection<LineUp> _lineup;

        public ObservableCollection<LineUp> LineUps
        {
            get {   
                //_lineup = LineUp.GetLineUps();
                return _lineup; }
            set { _lineup = value; }
        }
        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get { return _bands; }
            set { _bands = value; }
        }
        private ObservableCollection<INameId> _stages;

        public ObservableCollection<INameId> Stages
        {
            get { _stages = Stage.Waardes(); return _stages; }
            set { _stages = value; OnPropertyChanged("Stages"); }
        }
        private ObservableCollection<INameId> _genres;

        public ObservableCollection<INameId> Genres
        {
            get { _genres = Genre.Waardes(); return _genres; }
            set { _genres = value; }
        }
         private Band _geselecteerdeBand;

         public Band GeselecteerdeBand
         {
             get { return _geselecteerdeBand; }
             set
             {
                 _geselecteerdeBand = value;


                 //alle controls die hier aan gelinkt zijn worden geupdate
                 OnPropertyChanged("GeselecteerdeBand");
             }
         }
         private Genre _selectedGenreForBand;

         public Genre SelectedGenreForBand
         {
             get { return _selectedGenreForBand; }
             set { _selectedGenreForBand = value; OnPropertyChanged("SelectedGenreForBand"); }
         }
        
    }
}
