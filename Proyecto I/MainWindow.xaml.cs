using Order_Deliver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Proyecto_I
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Conexiones conexion = new Conexiones();
        private bool primera = true;
        public MainWindow()
        {
            InitializeComponent();

           
            //new Cliente("13", "Carlos").Show();
          


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Iniciar_Click(object sender, RoutedEventArgs e)
        {

            DataTable dt = conexion.ComprobarLogin(usuario.Text, contrasena.Password.ToString());
            string rol = string.Empty;


            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    rol = row["rol"].ToString();
                }
                switch (rol)
                {
                    case "0":
                        //administrador
                        
                        new Administrador(dt.Rows[0].ItemArray[0].ToString()).Show();
                        this.Close();
                        break;
                    case "1":
                        //Restaurante
                        
                        new Restaurante(dt.Rows[0].ItemArray[0].ToString()).Show();
                        this.Close();
                        break;
                    case "2":
                        //Cliente
                        
                        new Cliente(dt.Rows[0].ItemArray[0].ToString(), dt.Rows[0].ItemArray[1].ToString()).Show();
                        this.Close();
                        break;
                    case "3":
                        //Cocina                       
                        new Cocina(dt.Rows[0].ItemArray[0].ToString()).Show();
                        this.Close();
                        break;
                }
            }else
            {
                DialogResult r = (DialogResult)System.Windows.MessageBox.Show("Usuario o contraseña no valido", "Error");

            }



        }

      
        private void contrasena_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
            if (primera)
            {
                contrasena.Clear();
                primera = false;
            }
           
        }

        private void usuario_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (usuario.Text == "Usuario")
            {
                usuario.Clear();
            }
            
        }

        private void Label_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            new Registro().Show();
        }

        private void usuario_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Iniciar_Click(sender,e);
            }
        }

        private void contrasena_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Iniciar_Click(sender, e);
            }
        }

        private void contrasena_GotFocus(object sender, RoutedEventArgs e)
        {
            if (primera)
            {
                contrasena.Clear();
                primera = false;
            }
        }
    } 
}
