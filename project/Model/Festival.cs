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
    class Festival : ObservableObject
    {
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value;
            OnPropertyChanged("StartDate");
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged("EndDate"); }
        }

        private ObservableCollection<DateTime> _festivalDays;

        public ObservableCollection<DateTime> FestivalDays
        {
            get
            {

                return _festivalDays;
            }
            set
            {
                _festivalDays = value;
                OnPropertyChanged("FestivalDays");
            }
        }
        

        public static Festival HaalDatum()
        {
            Festival lijst;
            string sql = "SELECT * FROM festivalgeneral";
            DbDataReader Reader = DataBase.GetData(sql);
            Reader.Read();
          lijst = new Festival() { StartDate = (DateTime)Reader["StartDate"],EndDate = (DateTime)Reader["EndDate"]};
          lijst.FestivalDays = new ObservableCollection<DateTime>();
          for (var dt = lijst.StartDate; dt <= lijst.EndDate; dt = dt.AddDays(1))
          {
              lijst.FestivalDays.Add(dt);
          }   
                
             
            
            return lijst;
        }
    
    
        public static void Edit(Festival temp)
        {

            string sql = "UPDATE festivalgeneral SET StartDate = @StartDate, EndDate = @EndDate where ID = 1";
            DbParameter Startpar = DataBase.AddParameter("@StartDate", temp.StartDate);
            DbParameter Endpar = DataBase.AddParameter("@EndDate", temp.EndDate);
            DataBase.ModifyData(sql, Startpar, Endpar);
        }
    }
}
