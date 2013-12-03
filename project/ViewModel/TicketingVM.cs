using GalaSoft.MvvmLight.Command;
using project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace project.ViewModel
{
    // dit is de viewmodel class van de view usercontrol(wpf) Ticketing
    class TicketingVM : ObservableObject, IPage
    {

        public string Name
        {
            get { return "Ticketing"; }  //unieke naam!
        }
        private TicketType _geselecteerdeTicketType;

        public TicketType GeselecteerdeTicketType
        {
            get { return _geselecteerdeTicketType; }
            set { _geselecteerdeTicketType = value; OnPropertyChanged("GeselecteerdeTicketType"); }
        }
        
        public TicketingVM()
        {
            //haal de data op vanuit de model
            _tickets = Ticket.Waardes();
            _tickettypes = TicketType.GetWaardes();

            //maak de commands aan voor de buttons
            CreateSearchCommand();
            CreateCreateCommand();
            CreateReserveerCommand();
        
        }
        private ObservableCollection<Ticket> _tickets;

        public ObservableCollection<Ticket> Tickets
        {
            get { return _tickets; }
            set { _tickets = value; }
        }
        private ObservableCollection<TicketType> _tickettypes;

        public ObservableCollection<TicketType> TicketTypes
        {
            get { return _tickettypes; }
            set { _tickettypes = value; }
        }

        public ICommand SearchCommand
        {
            get;
            internal set;

        }
        private bool CanExecuteSearchCommand()
        {
            if (GeselecteerdeTicketType != null)
            {
                return true;

            }
            else
            {
                return false;
            }
        
        }
        private void CreateSearchCommand()
        {

            SearchCommand = new RelayCommand(ExecuteSearchCommand, CanExecuteSearchCommand);
        }
        private void ExecuteSearchCommand()
        {
            Tickets = Ticket.Search(GeselecteerdeTicketType.ID);
            OnPropertyChanged("Tickets");
        }

        public ICommand CreateCommand
        {
            get;
            internal set;

        }
        private bool CanExecuteCreateCommand(object[] param)
        {
            return true;

        }
        private void CreateCreateCommand()
        {

            CreateCommand = new RelayCommand<object[]>(ExecuteCreateCommand, CanExecuteCreateCommand);
        }
            private void ExecuteCreateCommand(object[] param)
            {
                TicketType.Add(param[0].ToString(),double.Parse(param[1].ToString()),int.Parse(param[2].ToString()));
                TicketTypes = TicketType.GetWaardes();
                OnPropertyChanged("TicketTypes");
            }
      
    

    public ICommand ReserveerCommand
        {
            get;
            internal set;

        }
        private bool CanExecuteReserveerCommand(object[] param)
        {
            if (param != null)
            {
                if (param[0] != null && param[1] != "naam")
                {
                    return true;
                }
                else
                {
                    return false;
                
                }

            }
            else
            {

                return false;
            }

        }
        private void CreateReserveerCommand()
        {

            ReserveerCommand = new RelayCommand<object[]>(ExecuteReserveerCommand, CanExecuteReserveerCommand);
        }
            private void ExecuteReserveerCommand(object[] param){

                Ticket.Add(param[0].ToString(), param[1].ToString(), int.Parse(param[2].ToString()), (TicketType)param[3]);
                Tickets = Ticket.Waardes();
                OnPropertyChanged("Tickets");
               
               
            }
      
    }
}
  
