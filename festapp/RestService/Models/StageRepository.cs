using DBHelper;
using models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace RestService.Models
{
    public class StageRepository
    {
        public static Stage GetbyID(int id)
        {
            List<Stage> lijst = new List<Stage>();
            string sql = "SELECT * FROM Stages WHERE ID = @ID";
            DbParameter par = Database.AddParameter("@ID", id);
            DbDataReader reader = Database.GetData(sql, par);
            Stage stage = null;
            while (reader.Read())
            {


                stage = new Stage() { Name = reader["name"].ToString(), ID = int.Parse(reader["ID"].ToString()) };

            }
            if (reader != null)
                reader.Close();
            return stage;
        }
    }
}