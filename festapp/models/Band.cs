using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace models
{
    public class Band
    {

        private string _iD;

        public string ID
        {
            get { return _iD; }
            set
            {
                _iD = value;
    
            }
        }


        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
   

            }
        }
        private string _picture;

        public string Picture
        {
            get { return _picture; }
            set
            {
                _picture = value;
             
            }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
              
            }
        }
        private string _facebook;
        
        public string Facebook
        {
            get { return _facebook; }
            set
            {
                _facebook = value;
                
            }
        }
        private string _twitter;

        public string Twitter
        {
            get { return _twitter; }
            set
            {
                _twitter = value;
        
            }
        }
        private List<Genre> _genres;

        public List<Genre> Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;

            }
        }

    }
}
