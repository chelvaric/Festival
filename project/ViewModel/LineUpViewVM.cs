using GalaSoft.MvvmLight.Command;
using project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace project.ViewModel
{
    class LineUpViewVM: ObservableObject,IPage   
    {

        public LineUpViewVM()
        {
           

            _lineUps = LineUp.GetLineUps();
            _stages = Stage.Waardes();
            _festivalDays = Festival.HaalDatum().FestivalDays;
        } 

        private ObservableCollection<LineUp> _lineUps;
      
        public ObservableCollection<LineUp> LineUps
        {
            get {   return _lineUps; }
            set { _lineUps = value; OnPropertyChanged("LineUps"); }
        }

        private ObservableCollection<INameId> _stages;

        public ObservableCollection<INameId> Stages
        {
            get { _stages = Stage.Waardes(); return _stages; }
            set { _stages = value; OnPropertyChanged("Stages"); }
        }

        private ObservableCollection<DateTime> _festivalDays;
        public ObservableCollection<DateTime> FestivalDays
        {
            get
            {
                _festivalDays = Festival.HaalDatum().FestivalDays;
                return _festivalDays;
            }
            set
            {
                _festivalDays = value;
                OnPropertyChanged("FestivalDays");
            }
        }

        private DateTime _geselecteerdeDatum;

        public DateTime GeselecteerdeDatum
        {
            get { return _geselecteerdeDatum; }
            set { _geselecteerdeDatum = value; OnPropertyChanged("GeselecteerdeDatum"); }
        }

        private INameId _geselecteerdeStage;

        public INameId GeselecteerdeStage
        {
            get { return _geselecteerdeStage; }
            set { _geselecteerdeStage = value; OnPropertyChanged("GeselecteerdeStage");  }
        }
        

        public ICommand FilterCommand {

            get { return new RelayCommand(Filter); }
        
        }
        public void Filter()
        {  //met ifs kijekn of er met 2 waarden moet gesorteerd worden of met 1
            if (GeselecteerdeDatum != DateTime.MinValue)
            {
                if (GeselecteerdeStage != null)
                {
                    LineUps = LineUp.GetLineUpsByStage("sorteer op Stage", "sorteer op Dag",GeselecteerdeDatum,GeselecteerdeStage);

                }
                else
                {
                    LineUps = LineUp.GetLineUpsByStage("", "sorteer op Dag", GeselecteerdeDatum, null);
                }

            }
            else
            {
                if (GeselecteerdeStage != null)
                {
                    LineUps.Clear();
                    LineUps = LineUp.GetLineUpsByStage("sorteer op Stage", "", GeselecteerdeDatum, GeselecteerdeStage);
                }
            
            }
           
        
        }

        public string Name
        {
            get { return "LineUpView"; }
        }
    }
}
