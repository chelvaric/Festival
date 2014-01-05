using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Model
{
    class Band : ObservableObject, IDataErrorInfo
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
        [Required]
        
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
        [Url]
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
        [Url]
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
        [Required]
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
        
        //haal de bands op
        public static ObservableCollection<Band> Bands()
        {
            try
            {

                ObservableCollection<Band> lijst = new ObservableCollection<Band>();
                string sql = "SELECT * FROM bands";
                DbDataReader Reader = DataBase.GetData(sql);
                while (Reader.Read())
                {  //leest de waarden uit en steekt ze in een model die dan aan de lijst word toegevoegd
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
            catch (Exception e)
            {

                Console.Write(e.Message);
            
                return new ObservableCollection<Band>();
            }
             
           
        }
        public static void AddGenres(ObservableCollection<Band> lijst)
        {

            foreach (Band item in lijst)
            {
                try
                {   //voegt de genres toe aan de model van elke band
                    ObservableCollection<Genre> tempLijst = new ObservableCollection<Genre>();
                    string sql = "SELECT bands_genres.*,genres.ID as GenreID,genres.name as GenreName FROM bands_genres INNER JOIN genres ON bands_genres.GenreID = genres.ID WHERE BandID = @BandID ";
                    DbParameter par = DataBase.AddParameter("@BandID", item.ID);
                    DbDataReader reader = DataBase.GetData(sql, par);
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
                catch (Exception e)
                {
                    Console.Write(e.Message);
            
                }
            }
            
        
        }
        public static void Add(string name,string pic,string descrp,string facebook, string twitter,ObservableCollection<Genre> genreID)
        { 
           //hier word een band toegevoegd
            string sql = "INSERT INTO bands(Name,Picture,Description,Facebook,Twitter) VALUES(@Name,@Picture,@Description,@Facebook,@Twitter)";
            ParamsMaken(name, pic, descrp, facebook, twitter, sql);
           ObservableCollection<Band> temp =  Bands();
         int BandID  = temp.Count();
         
         foreach (Genre genre in genreID)
         {
             //hier word er een link gelegd voor de band naar elke genre die erbij hoort
             sql = "INSERT INTO bands_genres(BandID,GenreID) VALUES(@BandID,@GenreID)";
             DbParameter parid = DataBase.AddParameter("@BandID", BandID);
             DbParameter pargenreid = DataBase.AddParameter("@GenreID", genre.ID);
             DataBase.ModifyData( sql, parid, pargenreid);
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
            if (BandID == null)
            {
                DataBase.ModifyData(sql, parName, parPic, parDescrp, parFacebook, parTwitter);
            }
            else
            {
                DataBase.ModifyData(sql, parName, parPic, parDescrp, parFacebook, parTwitter, parBandID);
            }
        }
        
        public static void Edit(Band editBand)
        {
            try
            {
                string sql;



                sql = "DELETE FROM bands_genres WHERE BandID = @BandID ";
                DbParameter parid = DataBase.AddParameter("@BandID", editBand.ID);

                DataBase.ModifyData(sql, parid);


                foreach (Genre genre in editBand.Genres)
                {

                    sql = "INSERT INTO bands_genres(BandID,GenreID) VALUES(@BandID,@GenreID)";
                    parid = DataBase.AddParameter("@BandID", editBand.ID);
                    DbParameter pargenreid = DataBase.AddParameter("@GenreID", genre.ID);
                    DataBase.ModifyData(sql, parid, pargenreid);
                }

                sql = "UPDATE bands SET Name = @Name, Picture = @Picture, Description = @Description,Facebook = @Facebook, Twitter=@Twitter Where ID = @BandID   ";
                ParamsMaken(editBand.Name, editBand.Picture, editBand.Description, editBand.Facebook, editBand.Twitter, sql, editBand.ID);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            
            }
            
        }

        private static void AddGenre(Band item)
        {
            ObservableCollection<Genre> tempLijst = new ObservableCollection<Genre>();
            string sql = "SELECT bands_genres.*,genres.ID as GenreID,genres.name as GenreName FROM bands_genres INNER JOIN genres ON bands_genres.GenreID = genres.ID WHERE BandID = @BandID ";
            DbParameter par = DataBase.AddParameter("@BandID", item.ID);
            DbDataReader reader = DataBase.GetData(sql, par);
            if (reader != null)
            {

                while (reader.Read())
                {


                    Genre temp = new Genre();
                    temp.ID = reader["GenreID"].ToString();
                    temp.Name = reader["GenreName"].ToString();
                    tempLijst.Add(temp);

                }
                if (reader != null)
                    reader.Close();
                item.Genres = tempLijst;

            }
        }
        public static Band BandByID(int id)
        {
            try
            {
                Band temp = new Band();
                string sql = "SELECT * FROM Bands WHERE ID = @ID";
                DbParameter par = DataBase.AddParameter("@ID", id);
                DbDataReader Reader = DataBase.GetData(sql, par);
                while (Reader.Read())
                {

                    temp.ID = Reader["ID"].ToString();
                    temp.Name = Reader["Name"].ToString();
                    temp.Description = Reader["Description"].ToString();
                    temp.Facebook = Reader["Facebook"].ToString();
                    temp.Picture = Reader["Picture"].ToString();
                    temp.Twitter = Reader["Twitter"].ToString();

                }
               
                AddGenre(temp);
                return temp;

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new Band();
                
            }
           

        }


        public string Error
        {
            get { return "verkeerde model"; }
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                    {
                        MemberName = columnName
                    });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null),
           null, true);
        }

        public void DeleteBand(int id)
        { 
        
        }
    }
}
