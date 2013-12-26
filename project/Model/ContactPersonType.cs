using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.ComponentModel.DataAnnotations;


namespace project.Model
{
    class ContactPersonType : ObservableObject,INameId
    {
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value;
         
            }
        }
          [Required(ErrorMessage = "give up a name")]
        private string _name;
      
        public string Name
        {
            get { return _name; }
            set { _name = value;
            OnPropertyChanged("Name");
            }
        }

        public static ObservableCollection<INameId> Waardes()
        {
            ObservableCollection<INameId> lijst = new ObservableCollection<INameId>();
            string sql = "SELECT * FROM contacttypes";
            DbDataReader reader = DataBase.GetData(sql);
            while (reader.Read())
            {


                lijst.Add(new ContactPersonType() {Name = reader["name"].ToString(),ID = reader["ID"].ToString() });
            
            }

            return lijst;

        }

        public static ContactPersonType search(string ID)
        {
            string sql = "SELECT * FROM contacttypes Where ID = @ID";
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
            string sql = "INSERT INTO contacttypes (name)  VALUES(@name)";
            DbParameter par = DataBase.AddParameter("@name", name);
            DataBase.ModifyData(sql, par);


        }
     
        public  void Edit(INameId temp)
        {
            string sql = "UPDATE contacttypes SET name= @name WHERE ID = @ID";
            DbParameter Name = DataBase.AddParameter("@name", temp.Name);
            DbParameter ID = DataBase.AddParameter("@ID", temp.ID);
            DataBase.ModifyData(sql, Name, ID);
        }
        public void Delete()
        {
            string sql = "DELETE FROM contacttypes WHERE  ID = @ID";

            DbParameter ID = DataBase.AddParameter("@ID", this.ID);
            DataBase.ModifyData(sql, ID);
        }
    }
}
