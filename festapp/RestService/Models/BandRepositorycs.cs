using DBHelper;
using models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace RestService.Models
{
    public class BandRepositorycs
    {
        public static ObservableCollection<Band> Bands()
        {


            ObservableCollection<Band> lijst = new ObservableCollection<Band>();
            string sql = "SELECT * FROM bands";
            DbDataReader Reader = Database.GetData(sql);
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
                List<Genre> tempLijst = new List<Genre>();
                string sql = "SELECT bands_genres.*,genres.ID as GenreID,genres.name as GenreName FROM bands_genres INNER JOIN genres ON bands_genres.GenreID = genres.ID WHERE BandID = @BandID ";
                DbParameter par = Database.AddParameter("@BandID", item.ID);
                DbDataReader reader = Database.GetData(sql, par);
                if (reader != null)
                {

                    while (reader.Read())
                    {


                        Genre temp = new Genre();
                        temp.ID = int.Parse(reader["GenreID"].ToString());
                        temp.Name = reader["GenreName"].ToString();
                        tempLijst.Add(temp);

                    }
                    item.Genres = tempLijst;

                }


            }
        }
    }
}