using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace models
{
    public class Genre
    {

        private int _iD;

        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        
    }
}
