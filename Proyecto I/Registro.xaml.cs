using Proyecto_I;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Order_Deliver
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        public Conexiones conexion = new Conexiones();
        private bool primera = true;
        private bool primera2 = true;
        public Registro()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool insertar = true;
            string error = "";
            if ((TextNombre.Text == "Nombre" || string.IsNullOrEmpty(TextNombre.Text))&&
                (TextApellidos.Text == "Apellidos" || string.IsNullOrEmpty(TextApellidos.Text))&&
                (TextAlias.Text == "Alias" || string.IsNullOrEmpty(TextAlias.Text))&&
                (TextCorreo.Text == "Correo" || string.IsNullOrEmpty(TextCorreo.Text))&&
                (Contrasena.Password.ToString() == "********" || string.IsNullOrEmpty(Contrasena.Password.ToString())) &&
                (Contrasena1.Password.ToString() == "********" || string.IsNullOrEmpty(Contrasena1.Password.ToString())))
            {
                insertar = false;
                
                error = "No puede haber ningún campo vacio";
            }
            else
            {
                if (!(Contrasena.Password.ToString() == Contrasena.Password.ToString()))
                {
                    insertar = false;
                    error += "Las contraseñas no coinciden.";
                }
                if (!TextCorreo.Text.Contains('@'))
                {
                    insertar = false;
                    error += "Escriba un correo valido.";
                }
                else
                {
                    if (conexion.ComprobacionCorreo(TextCorreo.Text))
                    {
                        insertar = false;
                        error += "Cuenta ya registrada con este correo.";
                    }
                }

                if (conexion.ComprobacionAlias(TextAlias.Text))
                {
                    insertar = false;
                    error += "Cuenta ya creada con este alias.";
                }


            }

            if (insertar)
            {
                if (conexion.InsertaCliente(TextNombre.Text, TextApellidos.Text, TextAlias.Text, TextCorreo.Text, Contrasena.Password.ToString()))
                {
                    DialogResult r = (DialogResult)System.Windows.MessageBox.Show("Se ha registrado correctamente", "Información");
                }
                else
                {
                    DialogResult r = (DialogResult)System.Windows.MessageBox.Show("Error en la inserción", "Error");
                }
            }
            else
            {
                DialogResult r = (DialogResult)System.Windows.MessageBox.Show(error, "Error");
            }
        }

        private void TextNombre_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (TextNombre.Text == "Nombre")
            {
                TextNombre.Clear();
            }
        }

        private void TextApellidos_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (TextApellidos.Text == "Apellidos")
            {
                TextApellidos.Clear();
            }
        }

        private void TextAlias_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (TextAlias.Text == "Alias")
            {
                TextAlias.Clear();
            }
        }

        private void TextCorreo_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (TextCorreo.Text == "Correo")
            {
                TextCorreo.Clear();
            }
        }

        private void Contrasena_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (primera)
            {
                Contrasena.Clear();
                primera = false;
            }
        }

        private void Contrasena1_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (primera2)
            {
                Contrasena1.Clear();
                primera2 = false;
            }
        }
    }
}
