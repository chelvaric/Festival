using project.Model;
using project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.ViewModel
{
    class LineUpVM:ObservableObject, IPage
    {
        public string Name
        {
            get { return "LineUp"; }
        }
        public LineUpVM() 
        {

            _lineup = LineUp.GeefWaardes();
            _bands = Band.Bands();
            _stages = Stage.Waardes();
            _genres = Genre.Waardes();
         }
        private ObservableCollection<LineUp> _lineup;

        public ObservableCollection<LineUp> LineUps
        {
            get { return _lineup; }
            set { _lineup = value; }
        }
        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get { return _bands; }
            set { _bands = value; }
        }
        private ObservableCollection<INameId> _stages;

        public ObservableCollection<INameId> Stages
        {
            get { return _stages; }
            set { _stages = value; }
        }
        private ObservableCollection<INameId> _genres;

        public ObservableCollection<INameId> Genres
        {
            get { return _genres; }
            set { _genres = value; }
        }
         private Band _geselecteerdeBand;

         public Band GeselecteerdeBand
         {
             get { return _geselecteerdeBand; }
             set
             {
                 _geselecteerdeBand = value;


                 //alle controls die hier aan gelinkt zijn worden geupdate
                 OnPropertyChanged("GeselecteerdeBand");
             }
         }
    }
}
