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
    class TicketType : ObservableObject
    {
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set { 
                _iD = value;
                OnPropertyChanged("ID");
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value;
            OnPropertyChanged("Name");
            }
        }

        private Double _price;

        public Double Price
        {
            get { return _price; }
            set { _price = value;
            OnPropertyChanged("Price");
            }
        }

        private int _avaibleTickets;

        public int AvaibleTickets
        {
            get { return _avaibleTickets; }
            set { _avaibleTickets = value;

            OnPropertyChanged("AvaibleTickets");
            
            }
        }

        public static ObservableCollection<TicketType> GetWaardes()
        {
            ObservableCollection<TicketType> lijst = new ObservableCollection<TicketType>();
            string sql = "SELECT * FROM tickettypes ";


            DbDataReader Reader = DataBase.GetData(sql);
            
            while (Reader.Read())
            {
                TicketType temp = new TicketType();
                maakTicketType(Reader, temp);
                lijst.Add(temp);
            }
            
            return lijst;
        }
        public static TicketType Search(string id, DbTransaction tran)
        {
            string sql = "SELECT * FROM tickettypes WHERE ID = @ID";
            DbParameter iD = DataBase.AddParameter("@ID", id);
           
            DbDataReader Reader = DataBase.GetData(tran,sql, iD);
            TicketType temp = new TicketType();
            while (Reader.Read())
            {

                maakTicketType(Reader, temp);
            }
            return temp;
        
        }

        private static void maakTicketType(DbDataReader Reader, TicketType temp)
        {
            temp.ID = Reader["ID"].ToString();
            temp.Name = Reader["Name"].ToString();
            temp.Price = double.Parse(Reader["Price"].ToString());
            temp.AvaibleTickets = int.Parse(Reader["Avaible"].ToString());
        }
        public static void Add(string name,Double price,int avaible)
        {
            TicketType temp = new TicketType() { Name = name, Price = price, AvaibleTickets = avaible };
            string sql = "INSERT INTO festival.tickettypes (Name,Price,Avaible) VALUES(@Name,@Price,@avaibletickets)";
            modify(temp, sql);
        }

        private static void modify(TicketType temp, string sql,DbParameter id = null)
        {
            DbParameter namepar = DataBase.AddParameter("@Name", temp.Name);
            DbParameter pricepar = DataBase.AddParameter("@Price", temp.Price);
            DbParameter avaiblepar = DataBase.AddParameter("@avaibletickets", temp.AvaibleTickets);
            DataBase.ModifyData(sql, namepar, pricepar, avaiblepar);

        }
        public static void Edit(TicketType temp)
        {
            string sql = "UPDATE tickettypes SET Name = @Name,Price=@Price,Avaible=@avaibletickets WHERE ID = @id";
            DbParameter ID = DataBase.AddParameter("@ID", temp.ID);
            modify(temp, sql,ID);
        
        }
        
    }
}
