using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace project.ViewModel
{
    class ApplicationVm : ObservableObject
    {

        public ApplicationVm()
        {

            _Pages = new ObservableCollection<IPage>();

            /*hieronder voegen we al een eerste IPage-object toe
            bij nieuwe pages moet deze lijst aangevuld worden met telkens de bij horende viewmodel klasse
             */
            _Pages.Add(new ParametersVM());
            _Pages.Add(new TicketingVM());
           
            
            _Pages.Add(new LineUpVM());
            _Pages.Add(new ContactVM());
            _Pages.Add(new LineUpViewVM());

            //default pages
            _currentPage = Pages[0];
        }


        private IPage _currentPage;

        public IPage CurrentPage
        {
            get
            {
                return _currentPage; //huidige page (dat nu getooond word)
            }
            set
            {
                _currentPage = value;
                //ik maak aan de buitenwereld beknd dat er een property CurrentPage
                //gewijzigd is Eventuele controls die gebind zijn worden nu aangepast
                OnPropertyChanged("CurrentPage");
            }
        }

        private ObservableCollection<IPage> _Pages;

        public ObservableCollection<IPage> Pages
        {
            get { return _Pages; }
            set { _Pages = value; }
        }

        // komt uit cursus!
        // deze 2 methodes worden gebruikt door de buttons (op de mainwindow.xaml)
        // en kan de juiste commando activeren hier is dat het uitwiselen van page
        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }

        }
        public void ChangePage(IPage page)
        {

            CurrentPage = page;

        }


    }
}
