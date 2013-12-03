using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ObservableCollection<LineUp> GeefWaardes()
        {
            ObservableCollection<LineUp> list = new ObservableCollection<LineUp>();
            list.Add(new LineUp() { From = "20:00", Till = "21:00" });
            return list;
        
        
        
        }

        public void Add()
        { }
        public void Delete()
        { }
        public void Edit()
        { }
    }
}
