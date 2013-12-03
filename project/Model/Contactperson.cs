using project.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace project.Model
{
    class Contactperson : ObservableObject
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
          
              
            }
        }
        private string _company;

        public string Company
        {
            get { return  _company; }
            set {  _company = value; }
        }
        private ContactPersonType _jobRole;

        public ContactPersonType JobRole
        {
            get { return _jobRole; }
            set { _jobRole = value; }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private string _cellPhone;

        public string CellPhone
        {
            get { return _cellPhone; }
            set { _cellPhone = value; }
        }

        private string _email;

        public string Email
        {
            get { return  _email; }
            set {  _email = value; }
        }

        public static ObservableCollection<Contactperson> GeefLijst()
        {
            ObservableCollection<Contactperson> lijst = new ObservableCollection<Contactperson>();
            
            string sql = "Select * from contactpersons";
            DbDataReader Reader = DataBase.GetData(sql);
            while(Reader.Read())
            {

                Contactperson temp = new Contactperson();
                MakeContact(Reader, temp);
                temp.JobRole = ContactPersonType.search(Reader["JobRoleID"].ToString());
                lijst.Add(temp);

            }
            return lijst;

        }

        private static void MakeContact(DbDataReader Reader, Contactperson temp)
        {
            temp.ID = Reader["ID"].ToString();
            temp.Name = Reader["Name"].ToString();
            temp._city = Reader["City"].ToString();
            temp._phone = Reader["Phone"].ToString();
            temp._email = Reader["Email"].ToString();
            temp._company = Reader["Company"].ToString();
            temp._cellPhone = Reader["CellPhone"].ToString();
        }
        public static void Add(Contactperson person)
        {
            string sql = "INSERT INTO contactpersons(Name,City,Phone,Email,CellPhone,Company,JobRoleID) VALUES (@Name,@City,@Phone,@Email,@CellPhone,@Company,@JobRoleID)";
            AddParams(person,sql);
        }

        private static void AddParams(Contactperson person,string sql, int ID = 0)
        {
          
                DbParameter id = DataBase.AddParameter("@ID", ID);
           
            DbParameter name = DataBase.AddParameter("@Name", person.Name);
            DbParameter city = DataBase.AddParameter("@City", person.City);
            DbParameter phone = DataBase.AddParameter("@Phone", person.Phone);
            DbParameter cellphone = DataBase.AddParameter("@CellPhone", person.CellPhone);
            DbParameter company = DataBase.AddParameter("@Company", person.Company);
            DbParameter email = DataBase.AddParameter("@email", person.Email);
            DbParameter jobRoleId = DataBase.AddParameter("@JobRoleId", person.JobRole.ID);
            DataBase.ModifyData(sql, name, city, phone, cellphone, company, email, id, jobRoleId);
        }
        public static Contactperson Search(string Name)
        {
            string sql = "Select * from contactpersons WHERE Name = @Name";
            DbParameter id = DataBase.AddParameter("@Name", Name);
            DbDataReader reader = DataBase.GetData(sql, id);
            Contactperson temp = new Contactperson();
            while (reader.Read())
            {
                MakeContact(reader, temp);
            
            }
            return temp;
        }
        public static ObservableCollection<Contactperson> SearchByType(string type)
        {
            ObservableCollection<Contactperson> templijst = new ObservableCollection<Contactperson>();
            string sql = "Select * from contactpersons WHERE JobRoleID = @JobRoleID";
            DbParameter typepar = DataBase.AddParameter("@JobRoleID", type);
            DbDataReader reader = DataBase.GetData(sql, typepar);
          
            while (reader.Read())
            {
                Contactperson temp = new Contactperson();
                MakeContact(reader, temp);
                templijst.Add(temp); 

            }
            return templijst;
        }
        public static void Edit(Contactperson person)
        {
            string sql = "UPDATE festival.contactpersons SET  Name = @Name, City = @City, Phone = @Phone, CellPhone = @CellPhone, Company = @Company, JobRoleID = @JobRoleId WHERE ID = @ID";
            AddParams(person,sql,int.Parse(person.ID));
        
        }
        public static void Delete(string id)
        {
            string sql = "DELETE FROM festival.contactpersons WHERE ID = @ID ";
            DbParameter Id = DataBase.AddParameter("@ID", id);
            DataBase.ModifyData(sql, Id);
        }
        
    }
}
