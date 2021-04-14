using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Order_Deliver
{
    public class Conexion
    {
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public Conexion()
        {

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

                return new MySqlConnection(connectionString);

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public DataTable Clientes()
        {
            MySqlConnection conn = Initialize();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM clientes", conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
            finally {
                conn.Close();
            }
        }


    }
}