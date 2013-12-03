using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Model
{
    class Band:ObservableObject
    {
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set
            {
                _iD = value;
                OnPropertyChanged("ID");
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");

            }
        }
        private string _picture;

        public string Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
                OnPropertyChanged("Picture");
            }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        private string _facebook;

        public string Facebook
        {
            get { return _facebook; }
            set
            {
                _facebook = value;
                OnPropertyChanged("Facebook");
            }
        }
        private string _twitter;

        public string Twitter
        {
            get { return _twitter; }
            set
            {
                _twitter = value;
                OnPropertyChanged("Twitter");
            }
        }
        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
                    
            }
        }


        public override string ToString()
        {
            return Name;
        }
        

        public static ObservableCollection<Band> Bands()
        {
           

                ObservableCollection<Band> lijst = new ObservableCollection<Band>();
                string sql = "SELECT * FROM bands";
                DbDataReader Reader = DataBase.GetData(sql);
                while (Reader.Read())
                {
                    Band temp = new Band();
                    temp.ID = Reader["ID"].ToString();
                    temp.Name = Reader["Name"].ToString();
                    temp.Description = Reader["Description"].ToString();
                    temp.Facebook = Reader["Facebook"].ToString();
                    temp.Picture = Reader["Picture"].ToString();
                    temp.Twitter = Reader["Twitter"].ToString();
                    lijst.Add(temp);
                }

                AddGenres(lijst);
                return lijst; 
          
        }
        public static void AddGenres(ObservableCollection<Band> lijst)
        {
           
            foreach (Band item in lijst)
            {
                ObservableCollection<Genre> tempLijst = new ObservableCollection<Genre>();
                string sql = "SELECT bands_genres.*,genres.ID as GenreID,genres.name as GenreName FROM festival.bands_genres INNER JOIN festival.genres ON bands_genres.GenreID = genres.ID WHERE BandID = @BandID ";
                DbParameter par = DataBase.AddParameter("@BandID",item.ID);
                    DbDataReader reader = DataBase.GetData(sql,par);
                    if (reader != null)
                    {

                        while (reader.Read())
                        {


                            Genre temp = new Genre();
                            temp.ID = reader["GenreID"].ToString();
                            temp.Name = reader["GenreName"].ToString();
                            tempLijst.Add(temp);

                        }
                        item.Genres = tempLijst;

                    }
                      
           
                }
          
        
        }
        public void Add()
        { 
           //hier komt de add
        
        }
        public void search()
        { }
        public void Edit()
        { }
   
    }
}
