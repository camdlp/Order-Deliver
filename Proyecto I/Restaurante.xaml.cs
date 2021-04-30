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

namespace Proyecto_I
{
    /// <summary>
    /// Lógica de interacción para Restaurante.xaml
    /// </summary>
    public partial class Restaurante : Window
    {
        public Conexiones conexion = new Conexiones();
        bool creando = false;
        bool modificando = false;
        public Restaurante(string id)
        {
            InitializeComponent();
            Restaurant.DataContext = conexion.Menu();
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Menu.IsEnabled = false;
            Cocina1.IsEnabled = false;
            

        }

        //GRID RESTAURANTE
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Cocina_Click(object sender, RoutedEventArgs e)
        {
            PantallaMenu.Visibility = Visibility.Hidden;
            PantallaCocina.Visibility = Visibility.Visible;
            EstadoCocina.DataContext = conexion.EstadoCocina();
        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            if (creando && !modificando)
            {
                if (!string.IsNullOrEmpty(TextBoxNombre.Text)  && !string.IsNullOrEmpty(TextBoxPrecios.Text))
                {
                    // Inserto el registro en la BBDD
                    conexion.InsertaPlato(TextBoxNombre.Text, CheckBoxDisponible.IsChecked, Convert.ToDouble(TextBoxPrecios.Text));
                    // Recargo los clientes.
                    Restaurant.DataContext = conexion.Menu();
                    // Protejo los campos contra escritura y los vacío
                    TextBoxNombre.IsReadOnly = TextBoxPrecios.IsReadOnly   = true;
                    TextBoxNombre.Text = TextBoxPrecios.Text  = string.Empty;
                    CheckBoxDisponible.IsEnabled = false;

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
                TextBoxNombre.IsReadOnly = TextBoxPrecios.IsReadOnly = CheckBoxDisponible.IsEnabled = false;
                TextBoxNombre.Text = TextBoxPrecios.Text = string.Empty;
                CheckBoxDisponible.IsEnabled = true;
                TextBoxIdPlato.Text = "Autoasignado";
                Crear.Content = "Pulsar para crear";
                creando = true;
            }
        }

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            if (!creando && modificando)
            {
                if (!string.IsNullOrEmpty(TextBoxNombre.Text) && !string.IsNullOrEmpty(TextBoxPrecios.Text) && !string.IsNullOrEmpty(TextBoxIdPlato.Text))
                {

                    // Modifico el registro en la BBDD
                    conexion.ModificaPlato(TextBoxIdPlato.Text, TextBoxNombre.Text, CheckBoxDisponible.IsChecked, Convert.ToDouble(TextBoxPrecios.Text));
                    // Recargo los clientes.
                    Restaurant.DataContext = conexion.Menu();
                    // Protejo los campos contra escritura
                    TextBoxNombre.IsReadOnly = TextBoxPrecios.IsReadOnly = TextBoxIdPlato.IsReadOnly = true;
                    TextBoxNombre.Text = TextBoxPrecios.Text = TextBoxIdPlato.Text = string.Empty;
                    CheckBoxDisponible.IsEnabled = false;

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

                if (!string.IsNullOrEmpty(TextBoxIdPlato.Text))
                {
                    // Desprotejo los campos contra escritura
                    TextBoxNombre.IsReadOnly = TextBoxPrecios.IsReadOnly  = TextBoxIdPlato.IsReadOnly = false;
                    CheckBoxDisponible.IsEnabled = true;
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
            if (!string.IsNullOrEmpty(TextBoxIdPlato.Text))
            {
                // Pido confirmación para eliminar el usuario.
                if (System.Windows.MessageBox.Show("¿Seguro que quieres eliminar el plato " + TextBoxNombre.Text + "? Esta acción es irreversible.", "Advertencia", (MessageBoxButton)MessageBoxButtons.YesNo) == MessageBoxResult.Yes)
                {
                    conexion.EliminaPlato(TextBoxIdPlato.Text, TextBoxNombre.Text);
                    Restaurant.DataContext = conexion.Menu();
                }

            }
            else
            {
                DialogResult r = (DialogResult)System.Windows.MessageBox.Show("Selecciona un campo para eliminar", "Error");
            }
        }
        private void Restaurant_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int i = 0;
            foreach (var item in e.AddedCells)
            {
                var col = item.Column as DataGridColumn;
                var fc = col.GetCellContent(item.Item);

                if (fc is TextBlock || fc is System.Windows.Controls.CheckBox)
                {
                    //TextBoxIdUsuario.Text += (fc as TextBlock).Text;
                    switch (i)
                    {
                        case 0:
                            TextBoxIdPlato.Text = (fc as TextBlock).Text;
                            break;
                        case 1:
                            TextBoxNombre.Text = (fc as TextBlock).Text;
                            break;
                        case 2:
                            if ((fc as System.Windows.Controls.CheckBox).IsChecked == true)
                            {
                                CheckBoxDisponible.IsChecked = true;
                            }
                            else
                            {
                                CheckBoxDisponible.IsChecked = false;
                            }
                          
                            break;
                        case 3:
                            TextBoxPrecios.Text = (fc as TextBlock).Text;
                            break;
                      
                        default:
                            break;
                    }
                    i++;
                }

            }
        }

        //GRID COCINA
        private void Menu1_Click(object sender, RoutedEventArgs e)
        {
            PantallaMenu.Visibility = Visibility.Visible;
            PantallaCocina.Visibility = Visibility.Hidden;
            Restaurant.DataContext = conexion.Menu();

        }

        private void Cocina1_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
