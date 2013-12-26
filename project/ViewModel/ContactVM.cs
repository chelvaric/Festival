using GalaSoft.MvvmLight.Command;
using project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace project.ViewModel
{
    class ContactVM:ObservableObject, IPage
    {
        public string Name
        {
            get {return "Contact"; }
        }


        public ContactVM()
        {
            _contacts = Contactperson.GeefLijst();
            _types = ContactPersonType.Waardes();
            CreateAddCommand();
            CreateEditCommand();
            CreateDeleteCommand();
            CreateSearchCommand();
            CreateFilterCommand();
        
        }

        private ObservableCollection<Contactperson> _contacts;

        public ObservableCollection<Contactperson> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; }
        }
        private ObservableCollection<INameId> _types;

        public ObservableCollection<INameId> Types
        {
            get { return _types; }
            set { _types = value; }
        }
        private Contactperson _geselecteerdeItem;

        public Contactperson GeselecteerdeItem
        {
            get { return _geselecteerdeItem; }
            set
            {
                _geselecteerdeItem = value;
                //if (GeselecteerdeItem != null)
                //{

                //    for (int i = 0; i < Types.Count; i++)
                //    {
                //        if (Types[i].Name == _geselecteerdeItem.JobRole.Name)
                //        {
                //            GeselecteerdeIndex = int.Parse(Types[i].ID);
                //        }
                //    }

                //}
                //alle controls die hier aan gelinkt zijn worden geupdate
                OnPropertyChanged("GeselecteerdeItem");
             
               
            }
        }

        private int _geselecteerdeIndex;

        public int GeselecteerdeIndex
        {
            get { return _geselecteerdeIndex; }
            set { _geselecteerdeIndex = value; OnPropertyChanged("GeselecteerdeIndex"); }
        }
        

        private ContactPersonType _geselecteerdeMainType;

        public ContactPersonType GeselecteerdeMainType
        {
            get { return _geselecteerdeMainType; }
            set
            {
                _geselecteerdeMainType = value;


                //alle controls die hier aan gelinkt zijn worden geupdate
                OnPropertyChanged("GeselecteerdeMainType");
            }
        }
        public ICommand AddCommand
        {
            get;
            internal set;
        }
        public bool CanxecuteAddCommand(object[] param)
        {
            if (param != null )
            {
                if (param[0].ToString() != "")
                {
                    return true;
                }
                else return false;

            }
            else { return false; }
        
        
        }
        public void ExecuteAddCommand(object[] param)
        {
            Contactperson temp = new Contactperson() { Name = param[0].ToString(), City = param[1].ToString(), Phone = param[2].ToString(), CellPhone = param[3].ToString(), Email = param[4].ToString(), Company = param[5].ToString(), JobRole = (ContactPersonType)param[6] };
            Contactperson.Add(temp);
            _contacts = Contactperson.GeefLijst();
            OnPropertyChanged("Contacts");
        
        }
        public void CreateAddCommand()
        {

            AddCommand = new RelayCommand<object[]>(ExecuteAddCommand, CanxecuteAddCommand);
        }
        public ICommand EditCommand
        {
            get;
            internal set;
        }

        public bool CanxecuteEditCommand()
        {
            if ( GeselecteerdeItem != null)
            {
                return true;

            }
            else { return false; }


        }
        public void ExecuteEditCommand()
        {
            Contactperson temp = new Contactperson() { Name = GeselecteerdeItem.Name, City = GeselecteerdeItem.City, Phone = GeselecteerdeItem.Phone, CellPhone = GeselecteerdeItem.CellPhone, Email = GeselecteerdeItem.Email, Company = GeselecteerdeItem.Company, JobRole = GeselecteerdeItem.JobRole,ID = GeselecteerdeItem.ID };
            Contactperson.Edit(temp);
            _contacts = Contactperson.GeefLijst();
           
            OnPropertyChanged("Contacts");

        }
        public void CreateEditCommand()
        {

            EditCommand = new RelayCommand(ExecuteEditCommand, CanxecuteEditCommand);
        }

        public ICommand DeleteCommand
        {
            get;
            internal set;
        }

        public bool CanxecuteDeleteCommand()
        {
            if (GeselecteerdeItem != null)
            {
                return true;

            }
            else { return false; }


        }
        public void ExecuteDeleteCommand()
        {
            Contactperson.Delete(GeselecteerdeItem.ID);
            _contacts = Contactperson.GeefLijst();
            OnPropertyChanged("Contacts");

        }
        public void CreateDeleteCommand()
        {

            DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanxecuteDeleteCommand);
        }

        public ICommand SearchCommand
        {
            get;
            internal set;
        }

        public bool CanxecuteSearchCommand(object par)
        {
            if (par != null)
            {
                return true;

            }
            else { return false; }


        }
        public void ExecuteSearchCommand(object par)
        {
            _contacts.Clear();
            _contacts.Add(Contactperson.Search(par.ToString()));
            OnPropertyChanged("Contacts");

        }
        public void CreateSearchCommand()
        {

            SearchCommand = new RelayCommand<object>(ExecuteSearchCommand, CanxecuteSearchCommand);
        }



        public ICommand FilterCommand
        {
            get;
            internal set;
        }

        public bool CanxecuteFilterCommand(SelectionChangedEventArgs par)
        {
            if (par != null)
            {
                return true;

            }
            else { return false; }


        }
        public void ExecuteFilterCommand(SelectionChangedEventArgs par)
        {
            _contacts.Clear();
            ContactPersonType temp = (ContactPersonType)par.AddedItems[0];

            _contacts = Contactperson.SearchByType(temp.ID);
            OnPropertyChanged("Contacts");

        }
        public void CreateFilterCommand()
        {

            FilterCommand = new RelayCommand<SelectionChangedEventArgs>(ExecuteFilterCommand, CanxecuteFilterCommand);
        }
    }
}
