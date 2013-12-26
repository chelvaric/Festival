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
    class Stage : ObservableObject,INameId
    {


       
        private string _id;
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
             
            }
        }
      
        private string _name;
        public string Name
        {
            get
            {
                  return _name;
            }
            set
            {
                   _name = value;
                   OnPropertyChanged("Name");
            }
        }

      




       public static ObservableCollection<INameId> Waardes()
       {
           ObservableCollection<INameId> lijst = new ObservableCollection<INameId>();
           string sql = "SELECT * FROM Stages";
           DbDataReader reader = DataBase.GetData(sql);
           while (reader.Read())
           {


               lijst.Add(new Stage() { Name = reader["name"].ToString(), ID = reader["ID"].ToString() });

           }

           return lijst;
       }

       public void Add(string name)
       {
           string sql = "INSERT INTO Stages (Name)  VALUES(@name)";
           DbParameter par = DataBase.AddParameter("@name", name);
           DataBase.ModifyData(sql, par);


       }
   
       public void Edit(INameId temp)
       {
           string sql = "UPDATE stages SET Name= @name WHERE ID = @ID";
           DbParameter Name = DataBase.AddParameter("@name", temp.Name);
           DbParameter ID = DataBase.AddParameter("@ID", temp.ID);
           DataBase.ModifyData(sql, Name, ID);
       }
   
            public void Delete()
        {
            string sql = "DELETE FROM stages WHERE  ID = @ID";

            DbParameter ID = DataBase.AddParameter("@ID", this.ID);
            DataBase.ModifyData(sql, ID);
        }

            public static Stage GetbyID(int id)
            {
                ObservableCollection<Stage> lijst = new ObservableCollection<Stage>();
                string sql = "SELECT * FROM Stages WHERE ID = @ID";
                DbParameter par = DataBase.AddParameter("@ID", id);
                DbDataReader reader = DataBase.GetData(sql, par);
                Stage stage = null;
                while (reader.Read())
                {


                    stage = new Stage() { Name = reader["name"].ToString(), ID = reader["ID"].ToString() };

                }
                if (reader != null)
                    reader.Close();
                return stage;
            }

    }
     
}
