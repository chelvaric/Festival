using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;


namespace project.Model
{
    class ContactPersonType : INameId
    {
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value;
         
            }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value;
           
            }
        }

        public static ObservableCollection<INameId> Waardes()
        {
            ObservableCollection<INameId> lijst = new ObservableCollection<INameId>();
            string sql = "SELECT * FROM Festival.ContactTypes";
            DbDataReader reader = DataBase.GetData(sql);
            while (reader.Read())
            {


                lijst.Add(new ContactPersonType() {Name = reader["name"].ToString(),ID = reader["ID"].ToString() });
            
            }

            return lijst;

        }

        public static ContactPersonType search(string ID)
        {
            string sql = "SELECT * FROM ContactTypes Where ID = @ID";
            DbParameter par = DataBase.AddParameter("@ID", ID);
            DbDataReader Reader = DataBase.GetData(sql, par);
            ContactPersonType temp = null;
            while (Reader.Read())
            {
                temp = new ContactPersonType() { Name = Reader["name"].ToString(), ID = Reader["ID"].ToString() };
            
            }

            return temp;
        }


        public  void Add(string name)
        {
            string sql = "INSERT INTO Festival.ContactTypes (name)  VALUES(@name)";
            DbParameter par = DataBase.AddParameter("@name", name);
            DataBase.ModifyData(sql, par);


        }
        public void Delete()
        { }
        public  void Edit(INameId temp)
        {
            string sql = "UPDATE contacttypes SET name= @name WHERE ID = @ID";
            DbParameter Name = DataBase.AddParameter("@name", temp.Name);
            DbParameter ID = DataBase.AddParameter("@ID", temp.ID);
            DataBase.ModifyData(sql, Name, ID);
        }
    }
}
