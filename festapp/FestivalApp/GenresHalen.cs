using models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FestivalApp
{
    class GenresHalen
    {
        private List<Genre> _genre;

        public List<Genre> Genres
        {
            get { return _genre; }
            set { _genre = value; }
        }


       
    }
}
