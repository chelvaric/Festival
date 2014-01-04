using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace models
{
    public class LineUp
    {
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        private DateTime _from;
      
        public DateTime From
        {
            get { return _from; }
            set { _from = value; }
        }
        private DateTime _till;
         public DateTime Till
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
    }
}
