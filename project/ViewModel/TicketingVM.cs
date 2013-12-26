using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GalaSoft.MvvmLight.Command;
using project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private Ticket _geselecteerdeTicket;

        public Ticket GeselecteerdeTicket
        {
            get { return _geselecteerdeTicket; }
            set { _geselecteerdeTicket = value; OnPropertyChanged("GeselecteerdeTicket"); }
        }
        public TicketingVM()
        {
            //haal de data op vanuit de model
            _tickets = Ticket.Waardes();
            _tickettypes = TicketType.GetWaardes();
            GeselecteerdeTicketType = _tickettypes[0];
            //maak de commands aan voor de buttons
            CreateSearchCommand();
            CreateCreateCommand();
            CreateReserveerCommand();
        
        }
        private ObservableCollection<Ticket> _tickets;

        public ObservableCollection<Ticket> Tickets
        {
            get { _tickets = Ticket.Waardes(); return _tickets; }
            set { _tickets = value; }
        }
        private ObservableCollection<TicketType> _tickettypes;

        public ObservableCollection<TicketType> TicketTypes
        {
            get { _tickettypes = TicketType.GetWaardes(); return _tickettypes; }
            set { _tickettypes = value; }
        }


        public ICommand EditCommand
        {
            get { return new RelayCommand(Edit, GeselecteerdeTicketType.IsValid); }
        
        }
     
        
        
        public void Edit()
        {
            TicketType.Edit(GeselecteerdeTicketType);
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
                if (param[0] != null && param[1].ToString() != "naam")
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

                OnPropertyChanged("Tickets");
                OnPropertyChanged("TicketTypes");
               
               
            }

            public ICommand DeleteTypeCommand
            {
                get { return new RelayCommand(DeleteType, CanDeleteType); }
            
            }
            public void DeleteType()
            {
                GeselecteerdeTicketType.delete();
                OnPropertyChanged(" GeselecteerdeTicketType");
            
            }
            public bool CanDeleteType()
            {

                if (GeselecteerdeTicketType != null)
                { return true; }
                else { return false; }
            
            }
            public ICommand DeleteTicketCommand
            {
                get { return new RelayCommand(DeleteTicket, CanDeleteTicket); }

            }
            public void DeleteTicket()
            {
                GeselecteerdeTicket.Delete();
                OnPropertyChanged("Tickets");

            }
            public bool CanDeleteTicket()
            {

                if (GeselecteerdeTicket != null)
                { return true; }
                else { return false; }

            }

            public ICommand MaakTicketCommand
            {

                get { return new RelayCommand(MakeTicket, CanMakeTicket); }

            }
            public void MakeTicket()
            {
                
                    string filename = GeselecteerdeTicket.TicketHolder + ".docx";
                    File.Copy("template.docx", filename, true);

                    WordprocessingDocument newdoc = WordprocessingDocument.Open(filename, true);
                    IDictionary<String, BookmarkStart> bookmarks = new Dictionary<String, BookmarkStart>();
                    foreach (BookmarkStart bms in newdoc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                    {
                        bookmarks[bms.Name] = bms;
                    }

                    // KeyValuePair<string, int>

                    bookmarks["Name"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(GeselecteerdeTicket.TicketHolder)), bookmarks["Name"]);

                    bookmarks["Email"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(GeselecteerdeTicket.TicketHolderEmail)), bookmarks["Email"]);
                    bookmarks["TicketType"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(GeselecteerdeTicket.TicketType.Name)), bookmarks["TicketType"]);
                    bookmarks["Amount"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(GeselecteerdeTicket.Amount.ToString())), bookmarks["Amount"]);
                    bookmarks["Price"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text((GeselecteerdeTicket.Amount * GeselecteerdeTicket.TicketType.Price).ToString())), bookmarks["Price"]);

                    newdoc.Close();
                


            }

            public bool CanMakeTicket()
            {

                if (GeselecteerdeTicket != null)
                { return true; }
                else
                {return false;}
            }
           
    }
}
  
