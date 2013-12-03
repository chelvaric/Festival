using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // vnaav hier bijgeovoedgde usings
using System.Data.Common;
using System.Data;

namespace project.Model
{
    class DataBase
    {
        //vooraf: instellingen iphalen uit de config bestand
        private static ConnectionStringSettings ConnectionString
        {
            get
            {


                return ConfigurationManager.ConnectionStrings["project.Properties.Settings.ConnectionString"];

            }

        }
        //stap 1: connectie opvragen
        private static DbConnection GetConection()
        {

            DbConnection con = DbProviderFactories.GetFactory(ConnectionString.ProviderName).CreateConnection();
            con.ConnectionString = ConnectionString.ConnectionString;
            con.Open();
            return con;
        }
        //stap 2 connectie vrijgeven
        public static void ReleaseConnection(DbConnection con)
        {
            if (con != null)
            {
                con.Close();
                con = null;

            }
        }
        //stap 3 command gaan opstellen: sqlstrng en parameters doorgeven
        //opmerkin: keyword params laat toe deze methode op te roepen met slechts 
        //één parameter, namelijk de sql string
        private static DbCommand BuildCommand(String sql, params DbParameter[] parameters)
        {
            //intern legen we connectie met de database
            DbCommand command = GetConection().CreateCommand();
            //command -> boodshappenlijstje
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;
            if (parameters != null)
            {

                command.Parameters.AddRange(parameters);
            }

            return command;


        }
        //stap 3bis hulpmethode om parameters te maken
        //deze methode maakt een parameter aan (die dan later kan doorgegeven worden via de buildcommand)
        public static DbParameter AddParameter(string naam, object value)
        {
            //param zijn provider afhankelijk ben verplicht naar de factory terug te keeren specefiek voor mijn provider
            DbParameter par = DbProviderFactories.GetFactory(ConnectionString.ProviderName).CreateParameter();
            par.ParameterName = naam;
            par.Value = value;
            return par;
        }
        //stap 4A: dataophalen (select statements)
        public static DbDataReader GetData(string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            DbDataReader reader = null;
            try
            {
                command = BuildCommand(sql, parameters);
                //word naar de database gegaan en word met een data reader teruggekeerd
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                //teruggeven
                return reader;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                if (reader != null) reader.Close();
                if (command != null) ReleaseConnection(command.Connection);
                throw ex;
            }

        }
        //stap 4B database gaan wijzigen (insert delete update)
        public static int ModifyData(string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(sql, parameters);
                int aantalRijenGewijzigd = command.ExecuteNonQuery();

                ReleaseConnection(command.Connection);

                //aantal anagepaste/verwijderede/nieuwe rijen word teruggegeven
                //zo weet de gebruiker het
                return aantalRijenGewijzigd;

            }
            catch (Exception ex)
            {


                Console.WriteLine(ex.Message);

                if (command != null) ReleaseConnection(command.Connection);
                throw ex;

            }


        }
        //EXTRA: werken met transacties

        public static DbTransaction BeginTransaction()
        {

            DbConnection con = null;
            try
            {
                con = GetConection();
                return con.BeginTransaction();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (con != null) ReleaseConnection(con);
                throw ex;

            }

        }

        //stap 3 extra : command in functie van transactie
        public static int ModifyData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(trans, sql, parameters);
                int aantalRijenGewijzigd = command.ExecuteNonQuery();

                ReleaseConnection(command.Connection);

                //aantal anagepaste/verwijderede/nieuwe rijen word teruggegeven
                //zo weet de gebruiker het
                return aantalRijenGewijzigd;

            }
            catch (Exception ex)
            {


                Console.WriteLine(ex.Message);

                if (command != null) ReleaseConnection(command.Connection);
                throw ex;

            }


        }
        //stap 4 extra A: data ophalen binnen in een transactie
        public static DbDataReader GetData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            DbDataReader reader = null;
            try
            {
                command = BuildCommand(trans, sql, parameters);
                //word naar de database gegaan en word met een data reader teruggekeerd
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                //teruggeven
                return reader;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                if (reader != null) reader.Close();
                if (command != null) ReleaseConnection(command.Connection);
                throw ex;
            }

        }
        //stap 4 extra b: data wijzigen binnen in een transactie
        private static DbCommand BuildCommand(DbTransaction trans, String sql, params DbParameter[] parameters)
        {
            //intern legen we connectie met de database
            DbCommand command = GetConection().CreateCommand();
            //command -> boodshappenlijstje
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;
            command.Transaction = trans;
            if (parameters != null)
            {

                command.Parameters.AddRange(parameters);
            }

            return command;


        }
    }
}
