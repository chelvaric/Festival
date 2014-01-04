using DBHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Web;
using models;
namespace RestService.Models
{
    public class GenreRepository
    {

        public static ObservableCollection<Genre> Waardes()
        {
            ObservableCollection<Genre> lijst = new ObservableCollection<Genre>();
            string sql = "SELECT * FROM Genres";
            DbDataReader reader = Database.GetData(sql);
            while (reader.Read())
            {


                lijst.Add(new Genre() { Name = reader["name"].ToString(), ID = int.Parse(reader["ID"].ToString()) });

            }


            return lijst;
        }
    }
}