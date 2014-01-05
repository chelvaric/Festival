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
        private Band Leegeband = new Band();
        private LineUp LeegeLineUp = new LineUp { Band = new Band()};
        public LineUpVM() 
        {

            SelectedLineUp = new LineUp();
            SelectedLineUp.Band = Leegeband;
            SelectedLineUp.From = new DateTime(1900, 4, 30);
            SelectedLineUp.Till = new DateTime(1900, 4, 30);
            _bands = Band.Bands();
          
        
         }

        private DateTime _selectedDag;

        public DateTime SelectedDag
        {
            get { return _selectedDag; }
            set { _selectedDag = value; }
        }




        private String _imagePath;

        public String ImagePath
        {
            get {   return _imagePath; }
            set { _imagePath = value; OnPropertyChanged("ImagePath"); }
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
            SelectedLineUp.Band.Genres.Remove(GeselecteerdeGenreInAddedGenres);
        
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
            if (SelectedLineUp.Band.Genres != null)
            {
                SelectedBand.Genres.Add((Genre)par);
                SelectedBand.Genres.Add((Genre)par);
            }
            else {

                SelectedBand.Genres = new ObservableCollection<Genre>();
                SelectedBand.Genres.Add((Genre)par);
                OnPropertyChanged("SelectedBand"); 
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
            OnPropertyChanged("Bands");
        }
        public bool CanInsert(object[] param)
        {
            if (SelectedBand != null)
            {
                if (SelectedBand.Name != null && SelectedBand.Genres != null)
                {

                    return true;

                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            
            }
        
        }


        public ICommand EditBandCommand
        { get { return new RelayCommand(EditBand, CanEditBand); } }
        public bool CanEditBand()
        {
            if (SelectedBand != null)
            {
                if (SelectedBand.ID != null)
                {
                    return SelectedBand.IsValid();
                }
                else
                {
                    return false;
                }
            }
            else
            {

                return false;
            }

            //if (SelectedLineUp != null)
            //{
            //    if (SelectedLineUp.Band != null)
            //    {
            //        if (SelectedLineUp.Band.Name != null && SelectedLineUp.Band.Genres != null)
            //        {
            //            return true;

            //        }
            //        else { return false; }

            //    }
            //    else { return false; }
            //}
            //else
            //{
            //    return false;
            
            //}
        
        }
        public void EditBand()
        {
            try
            {
                //set leege waardes in de strings omdat de database geen null aanvaard
                if (SelectedLineUp.Band.Picture == null)
                {
                    SelectedLineUp.Band.Picture = "";
                }
                if (SelectedLineUp.Band.Description == null)
                {
                    SelectedLineUp.Band.Description = "";
                }
                if (SelectedLineUp.Band.Facebook == null)
                {
                    SelectedLineUp.Band.Facebook = "";
                }
                if (SelectedLineUp.Band.Twitter == null)
                {
                    SelectedLineUp.Band.Twitter = "";
                }
                Band.Edit(SelectedLineUp.Band);
            }
            catch (Exception e)
            {

                Console.Write(e.Message);
            
            }
        
        }


        private ObservableCollection<LineUp> _lineup;

        public ObservableCollection<LineUp> LineUps
        {
            get {   
                _lineup = LineUp.GetLineUps();
                return _lineup; }
            set { _lineup = value; OnPropertyChanged("LineUps"); }
        }
        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get { _bands = Band.Bands(); return _bands; }
            set { _bands = value; OnPropertyChanged("Bands"); }
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
        
         private Genre _selectedGenreForBand;

         public Genre SelectedGenreForBand
         {
             get { return _selectedGenreForBand; }
             set { _selectedGenreForBand = value; OnPropertyChanged("SelectedGenreForBand"); }
         }

         private LineUp _selectedLineUp;

         public LineUp SelectedLineUp
         {
             get { if (_selectedLineUp == null) { _selectedLineUp = LeegeLineUp; } return _selectedLineUp; }
             set { _selectedLineUp = value; OnPropertyChanged("SelectedLineUp"); OnPropertyChanged("SelectedBand"); }
         }

       

         public Band SelectedBand
         {
             get { if (SelectedLineUp == null) { return Leegeband; } else { return _selectedLineUp.Band; } }
             set { _selectedLineUp.Band = value;  OnPropertyChanged("SelectedBand"); }
         }
         


         public ICommand DeleteLineUpCommand
         {
             get { return new RelayCommand(DeleteLineUp, CanDeleteLineUp); }
         
         }
         public void DeleteLineUp()
         {

             LineUp.DeleteLineUp(SelectedLineUp.ID);
             SelectedLineUp = LeegeLineUp;
             OnPropertyChanged("LineUps");
         
         }
         public bool CanDeleteLineUp()
         {
             if (SelectedLineUp != null)
             {
                 if (SelectedLineUp.ID != null)
                 {

                     return true;
                 }
                 else
                 {
                     return false;
                 }
             }
             return false;
          
         }
         public ICommand AddLineUpCommand
         {
             get { return new RelayCommand(AddLineUp, CanAddLineUp); }
         }
         public void AddLineUp()
         {
             //line up toeveoegen
             try
             {
                 //voor zorgen dat de years in de minumum zitten van de database
                
                 LineUp.ADDLineUp(SelectedLineUp);
                 OnPropertyChanged("LineUps");
             }
             catch (Exception e)
             {
                 Console.Write(e.Message);
             }
         }
         public bool CanAddLineUp()
         {
             if (SelectedLineUp != null)
             {
                 if (SelectedLineUp.Band != null && SelectedLineUp.Stage != null)
                 {
                     if (!SelectedLineUp.Date.Equals(DateTime.MinValue))
                     {
                         //kijken of de eind tijd later ligt dan de begin tijd
                         if (SelectedLineUp.From.CompareTo(SelectedLineUp.Till) == -1)
                         {
                             return KijkOfTijdAlGekozenIs();

                         }
                         else
                         {
                             return false;
                         }
                     }
                     else
                     {
                         return false;

                     }

                 }
                 else
                 {
                     return false;

                 }
             }
             else
             {

                 return false;
             }
         }
             public bool KijkOfTijdAlGekozenIs()
             {
                 bool oke = true;

                 foreach (LineUp lijnup in LineUps)
                 {
                     if(lijnup.Date.Equals(SelectedLineUp.Date) &&  lijnup.ID != SelectedLineUp.ID && lijnup.Stage.ID == SelectedLineUp.Stage.ID)
                     {

                     if (lijnup.From.TimeOfDay<=SelectedLineUp.From.TimeOfDay && lijnup.Till.TimeOfDay > SelectedLineUp.From.TimeOfDay)
                     {
                         oke = false;
                     }
                     if (lijnup.From.TimeOfDay < SelectedLineUp.Till.TimeOfDay && lijnup.Till.TimeOfDay >= SelectedLineUp.Till.TimeOfDay)
                     {
                         oke = false;
                     }

                     }
                 }

                 return oke;
             
             }

             public ICommand EditLineUpCommand
             {

                 get { return new RelayCommand(EditLineUp,CanAddLineUp); }
             
             }
             public void EditLineUp()
             {

                 LineUp.EditLineUp(SelectedLineUp);
             
             }


             public ICommand DeleteBandCommand
             {
                 get { return new RelayCommand(DeleteBand,CanDeleteBand); }
             
             }

             public void DeleteBand()
             {
                 SelectedBand.DeleteBand(int.Parse(SelectedBand.ID));
                 OnPropertyChanged("Bands");
             }

             public bool CanDeleteBand()
             {
                 if (SelectedBand != null)
                 {
                     if (SelectedBand.ID != null)
                     {

                         return true;

                     }
                     else
                     {

                         return false;
                     }
                 }
                 else
                 {
                     return false;
                 }
             }

    }
}
