using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Model
{
    class Ticket : ObservableObject
    {
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set { _iD = value;
            OnPropertyChanged("ID");
            }
        }

        private string _ticketHolder;

        public string TicketHolder
        {
            get { return _ticketHolder; }
            set { _ticketHolder = value;
            OnPropertyChanged("TicketHolder");
            }
        }

        private string _ticketHolderEmail;

        public string TicketHolderEmail
        {
            get { return _ticketHolderEmail; }
            set { _ticketHolderEmail = value;
            OnPropertyChanged("TicketHolderEmail");
            }
        }

        private TicketType _ticketType;

        public TicketType TicketType
        {
            get { return _ticketType; }
            set { _ticketType = value;
            OnPropertyChanged("TicketType");
            }
        }

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value;
            OnPropertyChanged("Amount");
            }
        }

        public static ObservableCollection<Ticket> Waardes()
        {
            ObservableCollection<Ticket> lijst = new ObservableCollection<Ticket>();
            string sql = "SELECT * FROM tickets";
            DbTransaction tran = DataBase.BeginTransaction();
            DbDataReader Reader = DataBase.GetData(tran,sql);
            while (Reader.Read())
            {
                Ticket temp = new Ticket();
                MakeTicket(tran, Reader, temp);
                lijst.Add(temp);
            }
            return lijst;
        }

        private static void MakeTicket(DbTransaction tran, DbDataReader Reader, Ticket temp)
        {
            temp.ID = Reader["ID"].ToString();
            temp.TicketHolder = Reader["TicketHolder"].ToString();
            temp.TicketHolderEmail = Reader["TicketHolderEmail"].ToString();
            temp.Amount = (int)Reader["amount"];
            temp.TicketType = TicketType.Search(Reader["TicketTypeID"].ToString(), tran);
        }
        public static void Add(string naam , string email, int amount, TicketType type)
        {
            string sql = "INSERT INTO tickets(TicketHolder,TicketHolderEmail,amount,TicketTypeID) VALUES (@TicketHolder,@TicketHolderEmail,@amount,@TicketTypeID)";
            DbParameter parNaam = DataBase.AddParameter("@TicketHolder", naam);
            DbParameter parEmail = DataBase.AddParameter("@TicketHolderEmail", email);
            DbParameter parAmount = DataBase.AddParameter("@amount", amount);
            DbParameter parType = DataBase.AddParameter("@TicketTypeID", type.ID);
            DataBase.ModifyData(sql, parAmount, parEmail, parNaam, parType);
        }
   
        public void Edit()
        { }
        public static ObservableCollection<Ticket> Search(string ticketType)
        {
            ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>();
            string sql = "SELECT * FROM tickets  WHERE TicketTypeID = @ID";
            DbTransaction tran = DataBase.BeginTransaction();
            DbParameter par = DataBase.AddParameter("@ID", ticketType);
            DbDataReader Reader = DataBase.GetData(tran,sql, par);
            while (Reader.Read())
            {
                Ticket temp = new Ticket();
                MakeTicket(tran, Reader, temp);
                tickets.Add(temp);
            }

            return tickets;
        
        }
        
    }
}
