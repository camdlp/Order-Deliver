using System;
using MySql.Data.MySqlClient;

namespace Order_Deliver
{
    public class Conexion
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public Conexion()
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
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                connection = new MySqlConnection(connectionString);
                return connection;

            }
            catch (Exception e)
            {

                throw e;
            }

        }

    }
}