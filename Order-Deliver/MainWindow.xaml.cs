using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Order_Deliver
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Conexion conexion = new Conexion();
        
        public MainWindow()
        {
            InitializeComponent();
            String connectionString = "SERVER=order-deliver.cdizvox6kcwq.eu-west-3.rds.amazonaws.com;DATABASE=OD;UID=admin;PASSWORD=12345678;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("USE OD;SELECT * FROM clientes", connection);
            connection.Open();
            DataTable dt = new DataTable();

            dt.Load(cmd.ExecuteReader());

            connection.Close();

            dataGrid.DataContext = dt;
        }

    }

}
