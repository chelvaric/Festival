using DBHelper;
using models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace RestService.Models
{
    public class LineUpRepository
    {

        public static List<LineUp> GetLineUps()
        {
            List<LineUp> templijst = new List<LineUp>();
            string sql = "SELECT * FROM Festival.dbo.lineup";

            DbDataReader data = Database.GetData(sql);

            while (data.Read())
            {
                LineUp temp = new LineUp();

                temp.ID = data["ID"].ToString();
                temp.Date = (DateTime)data["Date"];
                temp.From = (DateTime)data["From"];
                temp.Till = (DateTime)data["Till"];
                temp.Stage = StageRepository.GetbyID(int.Parse(data["StageID"].ToString()));
                temp.Band = BandRepositorycs.BandByID(int.Parse(data["BandID"].ToString()));
                templijst.Add(temp);
            }
            if (data != null)
                data.Close();
            return templijst;

        }

    }
}