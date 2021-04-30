using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Proyecto_I
{
    /// <summary>
    /// Lógica de interacción para Administrador.xaml
    /// </summary>
    public partial class Administrador : Window
    {
        public Conexiones conexion = new Conexiones();
        bool creando = false;
        bool modificando = false;

        public Administrador( string id)
        {
            InitializeComponent();
            adminGrid.DataContext = conexion.Clientes();
            
        }

     
        // Saca los datos de la celda y los coloca en sus respectivos textbox
        private void adminGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int i = 0;
            foreach (var item in e.AddedCells)
            {
                var col = item.Column as DataGridColumn;
                var fc = col.GetCellContent(item.Item);

                if(fc is TextBlock)
                {
                    //TextBoxIdUsuario.Text += (fc as TextBlock).Text;
                    switch (i)
                    {
                        case 0:
                            TextBoxIdUsuario.Text = (fc as TextBlock).Text;
                            break;
                        case 1:
                            TextBoxNombre.Text = (fc as TextBlock).Text;
                            break;
                        case 2:
                            TextBoxApellidos.Text = (fc as TextBlock).Text;
                            break;
                        case 3:
                            TextBoxAlias.Text = (fc as TextBlock).Text;
                            break;
                        case 4:
                            TextBoxPass.Text = (fc as TextBlock).Text;
                            break;
                        case 5:
                            TextBoxEmail.Text = (fc as TextBlock).Text;
                            break;
                        case 6:
                            TextBoxRol.Text = (fc as TextBlock).Text;
                            break;
                        default:
                            break;
                    }
                    i++;
                }
                
            }
            

                   
        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            if (creando && !modificando)
            {
                if(TextBoxNombre.Text != string.Empty && TextBoxApellidos.Text != string.Empty && TextBoxEmail.Text != string.Empty && TextBoxPass.Text != string.Empty 
                    && TextBoxRol.Text != string.Empty && TextBoxAlias.Text != string.Empty)
                {
                    // Inserto el registro en la BBDD
                    conexion.InsertaCliente(TextBoxNombre.Text, TextBoxApellidos.Text, TextBoxAlias.Text, TextBoxPass.Text, TextBoxEmail.Text, TextBoxRol.Text);
                    // Recargo los clientes.
                    adminGrid.DataContext = conexion.Clientes();
                    // Protejo los campos contra escritura y los vacío
                    TextBoxNombre.IsReadOnly = TextBoxApellidos.IsReadOnly = TextBoxEmail.IsReadOnly = TextBoxPass.IsReadOnly = TextBoxRol.IsReadOnly = TextBoxAlias.IsReadOnly = true;
                    TextBoxNombre.Text = TextBoxApellidos.Text = TextBoxEmail.Text = TextBoxPass.Text = TextBoxRol.Text = TextBoxAlias.Text = TextBoxIdUsuario.Text = string.Empty;
                    // Cambia el texto del botón crear.
                    Crear.Content = "Crear";

                }
                else
                {
                    DialogResult r = (DialogResult)System.Windows.MessageBox.Show("Rellena todos los campos", "Campos vacíos");
                }
                creando = false;
            }
            else
            {
                // Desprotejo los campos contra escritura y los vacío
                TextBoxNombre.IsReadOnly = TextBoxApellidos.IsReadOnly = TextBoxEmail.IsReadOnly = TextBoxPass.IsReadOnly = TextBoxRol.IsReadOnly = TextBoxAlias.IsReadOnly = false;
                TextBoxNombre.Text = TextBoxApellidos.Text = TextBoxEmail.Text = TextBoxPass.Text = TextBoxRol.Text = TextBoxAlias.Text = string.Empty;
                TextBoxIdUsuario.Text = "Autoasignado";
                Crear.Content = "Vuelve a pulsar para crear";
                creando = true;
            }
            

        }

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            if (!creando && modificando)
            {
                if (TextBoxNombre.Text != string.Empty && TextBoxApellidos.Text != string.Empty && TextBoxEmail.Text != string.Empty && TextBoxPass.Text != string.Empty
                    && TextBoxRol.Text != string.Empty && TextBoxAlias.Text != string.Empty)
                {

                    // Modifico el registro en la BBDD
                    conexion.ModificaCliente(TextBoxIdUsuario.Text, TextBoxNombre.Text, TextBoxApellidos.Text, TextBoxAlias.Text, TextBoxPass.Text, TextBoxEmail.Text, TextBoxRol.Text);
                    // Recargo los clientes.
                    adminGrid.DataContext = conexion.Clientes();
                    // Protejo los campos contra escritura
                    TextBoxNombre.IsReadOnly = TextBoxApellidos.IsReadOnly = TextBoxEmail.IsReadOnly = TextBoxPass.IsReadOnly = TextBoxRol.IsReadOnly = TextBoxAlias.IsReadOnly = true;
                    // Cambia el texto del botón.
                    Modificar.Content = "Modificar";
                    modificando = false;

                }
                else
                {
                    DialogResult r = (DialogResult)System.Windows.MessageBox.Show("Rellena todos los campos", "Campos vacíos");
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(TextBoxIdUsuario.Text))
                {
                    // Desprotejo los campos contra escritura
                    TextBoxNombre.IsReadOnly = TextBoxApellidos.IsReadOnly = TextBoxEmail.IsReadOnly = TextBoxPass.IsReadOnly = TextBoxRol.IsReadOnly = TextBoxAlias.IsReadOnly = false;

                    Modificar.Content = "Vuelve a pulsar para modificar";
                    modificando = true;
                }
                else
                {
                    DialogResult r = (DialogResult)System.Windows.MessageBox.Show("Selecciona un campo para modificar", "Error");
                }
            }
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxIdUsuario.Text))
            {
                // Pido confirmación para eliminar el usuario.
                if (System.Windows.MessageBox.Show("¿Seguro que quieres eliminar el usuario " + TextBoxAlias.Text + "? Esta acción es irreversible.", "Advertencia", (MessageBoxButton)MessageBoxButtons.YesNo) == MessageBoxResult.Yes)
                {
                    conexion.EliminaCliente(TextBoxIdUsuario.Text, TextBoxAlias.Text);
                    adminGrid.DataContext = conexion.Clientes();
                }
               
            }
            else
            {
                DialogResult r = (DialogResult)System.Windows.MessageBox.Show("Selecciona un campo para eliminar", "Error");
            }
        }

        private void Cliente_Click(object sender, RoutedEventArgs e)
        {
            new Cliente("1", "Admin").Show();
        }

        private void Cocina_Click(object sender, RoutedEventArgs e)
        {
            new Cocina("").Show();
        }

        private void Restaurante_Click(object sender, RoutedEventArgs e)
        {
            new Restaurante("").Show();
        }
    }






}


