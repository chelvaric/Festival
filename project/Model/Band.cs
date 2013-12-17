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
        public static void Add(string name,string pic,string descrp,string facebook, string twitter,ObservableCollection<Genre> genreID)
        { 
           //hier komt de add
            string sql = "INSERT INTO bands(Name,Picture,Description,Facebook,Twitter) VALUES(@Name,@Picture,@Description,@Facebook,@Twitter)";
            ParamsMaken(name, pic, descrp, facebook, twitter, sql);
           ObservableCollection<Band> temp =  Bands();
         int BandID  = temp.Count();
         DbTransaction trans = DataBase.BeginTransaction();
         foreach (Genre genre in genreID)
         {
             
             sql = "INSERT INTO bands_genres(BandID,GenreID) VALUES(@BandID,@GenreID)";
             DbParameter parid = DataBase.AddParameter("@BandID", BandID);
             DbParameter pargenreid = DataBase.AddParameter("@GenreID", genre.ID);
             DataBase.ModifyData(trans, sql, parid, pargenreid);
         }
        }

        private static void ParamsMaken(string name, string pic, string descrp, string facebook, string twitter, string sql,string BandID = null)
        {
            DbParameter parName = DataBase.AddParameter("@Name", name);
            DbParameter parPic = DataBase.AddParameter("@Picture", pic);
            DbParameter parDescrp = DataBase.AddParameter("@Description", descrp);
            DbParameter parFacebook = DataBase.AddParameter("@Facebook", facebook);
            DbParameter parTwitter = DataBase.AddParameter("@Twitter", twitter);
            DbParameter parBandID = DataBase.AddParameter("@BandID", BandID);
            DataBase.ModifyData(sql, parName, parPic, parDescrp, parFacebook, parTwitter,parBandID);
        }
        
        public static void Edit(Band editBand)
        {
            string sql; 
            
            DbTransaction trans = DataBase.BeginTransaction();
            
                    sql = "DELETE FROM bands_genres WHERE BandID = @BandID ";
                    DbParameter parid = DataBase.AddParameter("@BandID", editBand.ID);
                  
                    DataBase.ModifyData(trans, sql, parid);


                    foreach (Genre genre in editBand.Genres)
                    {

                        sql = "INSERT INTO bands_genres(BandID,GenreID) VALUES(@BandID,@GenreID)";
                         parid = DataBase.AddParameter("@BandID", editBand.ID);
                        DbParameter pargenreid = DataBase.AddParameter("@GenreID", genre.ID);
                        DataBase.ModifyData(trans, sql, parid, pargenreid);
                    }
        
            sql = "UPDATE bands SET Name = @Name, Picture = @Picture, Description = @Description,Facebook = @Facebook, Twitter=@Twitter Where ID = @BandID   ";
            ParamsMaken(editBand.Name, editBand.Picture, editBand.Description, editBand.Facebook, editBand.Twitter,sql,editBand.ID);
       
            
        }
   
    }
}
