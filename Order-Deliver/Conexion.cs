using MySql.Data.MySqlClient;
using System;

namespace Order_Deliver
{
    public class Conexiones
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public Conexiones()
        {
            Initialize();
        }

        //Initialize values
        public MySqlConnection Initialize()
        {
            try
            {
                server = "order-deliver.cdizvox6kcwq.eu-west-3.rds.amazonaws.com";
                database = "OD";
                uid = "admin";
                password = "12345678";
                string conString;
                conString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                connection = new MySqlConnection(conString);
                return connection;

            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}