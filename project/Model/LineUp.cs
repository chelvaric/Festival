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
    class LineUp : ObservableObject
    {
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        private string _from;

        public string From
        {
            get { return _from; }
            set { _from = value; }
        }
        private string  _till;

        public string  Till
        {
            get { return _till; }
            set { _till = value; }
        }

        private Stage _stage;

        public Stage Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private Band _band;

        public Band Band
        {
            get { return _band; }
            set { _band = value; }
        }
        public override string ToString()
        {
            return "Start: " + From + " Till: " + Till ;
        }

        public static ObservableCollection<LineUp> GetLineUps()
        {
            ObservableCollection<LineUp> templijst = new ObservableCollection<LineUp>();
            string sql = "SELECT * FROM Festival.dbo.lineup";
          
            DbDataReader data = DataBase.GetData(sql);
         
            while (data.Read())
            {
                LineUp temp = new LineUp();

                temp.ID = data["ID"].ToString();
                temp.Date = (DateTime)data["Date"];
                temp.From = data["From"].ToString();
                temp.Till = data["Till"].ToString();
                temp.Stage =  Stage.GetbyID(int.Parse(data["StageID"].ToString()));
                temp.Band = Band.BandByID(int.Parse(data["BandID"].ToString()));
                templijst.Add(temp);
            }
            if (data != null)
                data.Close();
            return templijst;

        }

        public static ObservableCollection<LineUp> GetLineUpsByStage(string id, string date,DateTime datum,INameId stage)
        {
            string sql;


         
            DbParameter par;
            DbParameter parDate;
            DbDataReader data;
            if (id == "sorteer op Stage" && date == "sorteer op Dag")
            {
                sql = "SELECT * FROM Festival.dbo.lineup WHERE StageID =  @StageID AND Date =  @Date";
                par = DataBase.AddParameter("@StageID", stage.ID);
                parDate = DataBase.AddParameter("@Date", datum);
                data = DataBase.GetData(sql, par, parDate);
             
            }
            else
            {
                if (date == "sorteer op Dag")
                {

                    sql = "SELECT * FROM Festival.dbo.lineup WHERE  Date =  @Date";
    


                    parDate = DataBase.AddParameter("@Date",datum );
                    data = DataBase.GetData( sql, parDate);

                }
                else
                {

                    if (id == "sorteer op Stage")
                    {

                        sql = "SELECT * FROM Festival.dbo.lineup WHERE StageID =  @StageID ";
                        par = DataBase.AddParameter("@StageID", stage.ID);

                        data = DataBase.GetData(sql, par);
                    }
                    else
                    {
                        sql = "SELECT * FROM Festival.dbo.lineup";
                        data = DataBase.GetData(sql);
                        
                    }
                }
            }

            ObservableCollection<LineUp> templijst = new ObservableCollection<LineUp>();





            while (data.Read())
            {
                LineUp temp = new LineUp();

                temp.ID = data["ID"].ToString();
                temp.Date = (DateTime)data["Date"];
                temp.From = data["From"].ToString();
                temp.Till = data["Till"].ToString();
                temp.Stage = Stage.GetbyID(int.Parse(data["StageID"].ToString()));
                temp.Band = Band.BandByID(int.Parse(data["BandID"].ToString()));
                templijst.Add(temp);
            }
            if (data != null)
                data.Close();

            return templijst;

        }
      
    }
}
