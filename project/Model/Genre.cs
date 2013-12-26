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
    class Genre : ObservableObject, INameId
    {
        private string _iD;

        public string ID
        {
            get { return _iD; }
            set { _iD = value;
       
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

         



        public static ObservableCollection<INameId> Waardes()
         {
             ObservableCollection<INameId> lijst = new ObservableCollection<INameId>();
             string sql = "SELECT * FROM Genres";
             DbDataReader reader = DataBase.GetData(sql);
             while (reader.Read())
             {


                 lijst.Add(new Genre() { Name = reader["name"].ToString(), ID = reader["ID"].ToString() });

             }


            return lijst;
         }
        public void Add(string name)
        {
            string sql = "INSERT INTO Genres (name)  VALUES(@name)";
            DbParameter par = DataBase.AddParameter("@name", name);
            DataBase.ModifyData(sql, par);
            
        
        }
      
        public void Edit(INameId temp)
        {
            string sql = "UPDATE genres SET name= @name WHERE ID = @ID";
            DbParameter Name = DataBase.AddParameter("@name", temp.Name);
            DbParameter ID = DataBase.AddParameter("@ID", temp.ID);
            DataBase.ModifyData(sql, Name, ID);
        
        }
        public void Delete()
        {
            string sql = "DELETE FROM genres WHERE  ID = @ID";

            DbParameter ID = DataBase.AddParameter("@ID", this.ID);
            DataBase.ModifyData(sql, ID);
        }
    }
}
