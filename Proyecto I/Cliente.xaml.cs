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
using System.Windows.Shapes;

namespace Proyecto_I
{
    /// <summary>
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : Window
    {
        private Conexiones conexion = new Conexiones();
        private DataTable dtCesta = new DataTable();
        private string id_cliente;

        public Cliente(string idCliente, string alias)
        {
            InitializeComponent();
            // Recibo en el constructor del cliente el id del cliente que hace el pedido.
            id_cliente = idCliente;
            //id_cliente = 12;
            label.Content = "Bienvenido " + alias;
            GridPlatos.DataContext = conexion.Carta();

            // Añado las columnas al DT de la cesta.
            dtCesta.Columns.Add("Id");
            dtCesta.Columns.Add("Nombre");
            dtCesta.Columns.Add("Disp");
            dtCesta.Columns.Add("Precio");
        }

        private void Anadir_Click(object sender, RoutedEventArgs e)
        {
            if (GridPlatos.SelectedItem is null)
            {
                System.Windows.MessageBox.Show("No hay ningún plato seleccionado", "Error");
                
            }
            else
            {
                // Saco la información de la fila seleccionada
                Object[] obj = ((DataRowView)GridPlatos.SelectedItems[0]).Row.ItemArray;
                // Creo una nueva fila en el DT y añado la información
                //dtCesta.NewRow();
                dtCesta.Rows.Add(obj);
                GridCesta.DataContext = dtCesta;
                
            }
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (GridCesta.SelectedItem is null)
            {
                System.Windows.MessageBox.Show("No hay ningún plato seleccionado", "Error");

            }
            else
            {
                int platoSeleccionadoCesta = GridCesta.SelectedIndex;
                dtCesta.Rows.RemoveAt(platoSeleccionadoCesta);
                GridCesta.DataContext = dtCesta;

            }
        }

        private void Tramitar_Click(object sender, RoutedEventArgs e)
        {
            if (GridCesta.Columns.Count <= 0)
            {
                System.Windows.MessageBox.Show("No hay ningún plato seleccionado", "Error");

            }
            else
            {
                if(conexion.InsertaPedido(id_cliente, dtCesta))
                {
                    System.Windows.MessageBox.Show("Pedido completado.", "Completado.");
                    dtCesta.Rows.Clear();
                    GridCesta.DataContext = dtCesta;
                }

            }
        }

        
    }
}
