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
    class LineUp : ObservableObject, IDataErrorInfo
    {
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        private DateTime _from;
        [Required]
        public DateTime From
        {
            get { return _from; }
            set { _from = value; }
        }
        private DateTime  _till;
        [Required]
        public DateTime  Till
        {
            get { return _till; }
            set { _till = value; }
        }

        private Stage _stage;
        [Required]
        public Stage Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }

        private DateTime _date;
        [Required]
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private Band _band;
        [Required]
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
                temp.From = (DateTime)data["From"];
                temp.Till = (DateTime)data["Till"];
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

                    sql = "SELECT * FROM lineup WHERE  Date =  @Date";
    


                    parDate = DataBase.AddParameter("@Date",datum );
                    data = DataBase.GetData( sql, parDate);

                }
                else
                {

                    if (id == "sorteer op Stage")
                    {

                        sql = "SELECT * FROM lineup WHERE StageID =  @StageID ";
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
                temp.From = (DateTime)data["From"];
                temp.Till = (DateTime)data["Till"];
                temp.Stage = Stage.GetbyID(int.Parse(data["StageID"].ToString()));
                temp.Band = Band.BandByID(int.Parse(data["BandID"].ToString()));
                templijst.Add(temp);
            }
            if (data != null)
                data.Close();

            return templijst;

        }
        public static void DeleteLineUp(string id)
        {

            string sql = "DELETE FROM lineup WHERE ID = @ID";
            DbParameter parID = DataBase.AddParameter("@ID", id);
            DataBase.ModifyData(sql, parID);
        }

        public static void ADDLineUp(LineUp lineup)
        {
            string sql = "INSERT INTO lineup VALUES (@From,@Till,@Date,@Band,@Stage)";
            MakePars(lineup, sql);
        
        }
        public static void EditLineUp(LineUp lineup)
        {
            string sql = "UPDATE lineup SET From = @From,Till = @Till, Date= @Date, Stage = @Stage, Band = @Band WHERE ID = @ID";
            MakePars(lineup, sql,lineup.ID);
        
        }

        private static void MakePars(LineUp lineup, string sql,string ID="")
        {
            DbParameter parID = DataBase.AddParameter("@ID", lineup.ID);
            DbParameter parDate = DataBase.AddParameter("@Date", lineup.Date);
            DbParameter parFrom = DataBase.AddParameter("@From", lineup.From);
            DbParameter parTill = DataBase.AddParameter("@Till", lineup.Till);
            DbParameter parStage = DataBase.AddParameter("@Stage", lineup.Stage.ID);
            DbParameter parBand = DataBase.AddParameter("@Band", lineup.Band.ID);
            if (ID == null)
            {
                DataBase.ModifyData(sql, parDate, parFrom, parTill, parStage, parBand, parID);
            }
            else
            {
                DataBase.ModifyData(sql, parDate, parFrom, parTill, parStage, parBand);
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
    }
}
