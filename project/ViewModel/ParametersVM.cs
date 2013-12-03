using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using project.Model;
using project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace project.ViewModel
{
    class ParametersVM : ObservableObject, IPage
    {



        public string Name
        {
            get { return "Parameters"; }
        }
        public ParametersVM()
        {
            _datums = Festival.HaalDatum();
            _stages = Stage.Waardes();
            _Genres = Genre.Waardes();
            _ContactTypes = ContactPersonType.Waardes();
            CreateSaveCommand();
            CreateEditCommand();
            CreateDatumEditCommand();

        }

        public ObservableCollection<ObservableCollection<INameId>> Instellingen
        {
            get
            {
                ObservableCollection<ObservableCollection<INameId>> lijst = new ObservableCollection<ObservableCollection<INameId>>();
                lijst.Add(Stages);
                lijst.Add(ContactTypes);
                lijst.Add(Genres);
                return lijst;
            }

        }
        private ObservableCollection<INameId> _geselecteerdeinstelling;

        public ObservableCollection<INameId> GeselecteerdeInstelling
        {
            get { return _geselecteerdeinstelling; }
            set
            {
                _geselecteerdeinstelling = value;


                //alle controls die hier aan gelinkt zijn worden geupdate
                OnPropertyChanged("GeselecteerdeInstelling");
            }
        }
        private INameId _geselecteerdeItem;

        public INameId GeselecteerdeItem
        {
            get { return _geselecteerdeItem; }
            set
            {
                _geselecteerdeItem = value;


                //alle controls die hier aan gelinkt zijn worden geupdate
                OnPropertyChanged("GeselecteerdeItem");
            }
        }


        #region datum
        private Festival _datums;

        public Festival Datums
        {
            get { return _datums; }
            set
            {
                _datums = value;
                Festival.Edit(_datums);
                OnPropertyChanged("Datums");
            }
        }
        #endregion

        #region Stages


        private ObservableCollection<INameId> _stages;

        public ObservableCollection<INameId> Stages
        {
            get { return _stages; }
            set { _stages = value; }
        }

        #endregion

        private ObservableCollection<INameId> _Genres;

        public ObservableCollection<INameId> Genres
        {
            get { return _Genres; }
            set { _Genres = value; }
        }
        private ObservableCollection<INameId> _ContactTypes;

        public ObservableCollection<INameId> ContactTypes
        {
            get { return _ContactTypes; }
            set { _ContactTypes = value; }
        }

        public ICommand SaveCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteSaveCommand(object parameter)
        {
            if (GeselecteerdeInstelling != null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        private void CreateSaveCommand()
        {
            SaveCommand = new RelayCommand<object>(SaveExecute, CanExecuteSaveCommand);
        }

        public void SaveExecute(object parameter)
        {


            GeselecteerdeInstelling[0].Add(parameter.ToString());

        }


        public ICommand EditCommand
        {
            get;
            internal set;
        }

        private bool CanExecuteEditCommand()
        {
            if (GeselecteerdeItem != null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        private void CreateEditCommand()
        {
            EditCommand = new RelayCommand(EditExecute, CanExecuteEditCommand);
        }

        public void EditExecute()
        {


            GeselecteerdeItem.Edit(GeselecteerdeItem);

        }

        public ICommand DatumEditCommand
        {
            get;
            internal set;


        }
        private bool CanExecuteDatumEditCommand()
        {
            return true;
        }
        private void CreateDatumEditCommand()
        {
            DatumEditCommand = new RelayCommand(ExecuteDatumEditCommand, CanExecuteDatumEditCommand);
        }
        private void ExecuteDatumEditCommand()
        {

            Festival.Edit(Datums);
        
        }
    }
}
