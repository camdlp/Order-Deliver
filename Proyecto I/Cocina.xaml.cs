using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Proyecto_I
{
    /// <summary>
    /// Lógica de interacción para Cocina.xaml
    /// </summary>
    public partial class Cocina : Window
    {
        readonly Conexiones conexion = new Conexiones();
        DataTable dtp = new DataTable();
        DataTable dtpp = new DataTable();
        private Timer timerPedidos;

        public Cocina(string id)
        {
            InitializeComponent();

            // Creo un task alternativo que me cargue los datos en las tablas.
            // Lo hago así porque si no, esperaría eternamente a que hubiera
            // pedidos antes de mostrar la interfaz.
            Task.Factory.StartNew(() => CambiaPedidoPlato());

        }

        // Rellena las tablas de pedidos y platos del pedido. Contiene varios filtros
        // para hacer que las tablas se actualicen únicamente cuando haya información
        // nueva que mostrar.
        // Devuelve un bool especificando si quedan platos no preparados. De no haberlos, 
        // el pedido pasará automáticamente a completado y desaparecerá de la pantalla.
        private bool CambiaPedidoPlato()
        {
            // Copio las tablas anteriores para ver si las nuevas son iguales.
            DataTable dtpAntiguo = dtp.Copy();
            DataTable dtppAntiguo = dtpp.Copy();

            dtp = conexion.Pedidos();
            var hayNoHechos = false;

            if (dtp.Rows.Count != 0)
            {
                string primer_id = dtp.Rows[0][0].ToString();
                dtpp = conexion.PedidosPlatos(primer_id);

                if (!SonIgualesDT(dtp, dtpAntiguo) || TablaPedidos.Columns.Count == 0 || !SonIgualesDT(dtpp, dtppAntiguo))
                {
                    // Salgo momentáneamente de la tarea para acceder a los recursos principales
                    this.Dispatcher.Invoke(() =>
                    {
                        TablaPedidos.DataContext = dtp;
                        TablaPlatosPedidos.DataContext = dtpp;
                    });

                    // Compruebo si quedan platos no preparados
                    foreach (DataRow item in dtpp.Rows)
                    {
                        if (item[3].ToString() == "0")
                        {
                            hayNoHechos = true;
                        }
                    }
                }
            }
            else
            {
                // Salgo momentáneamente de la tarea para acceder a los recursos principales
                this.Dispatcher.Invoke(() =>
                {
                    TablaPlatosPedidos.DataContext = null;
                    TablaPedidos.DataContext = null;
                });

            }


            return hayNoHechos;
        }

        // Comprueba si 2 DataTables son iguales.
        public static bool SonIgualesDT(DataTable tbl1, DataTable tbl2)
        {
            if (tbl1.Rows.Count != tbl2.Rows.Count || tbl1.Columns.Count != tbl2.Columns.Count)
                return false;


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                for (int c = 0; c < tbl1.Columns.Count; c++)
                {
                    if (!Equals(tbl1.Rows[i][c], tbl2.Rows[i][c]))
                        return false;
                }
            }
            return true;
        }

        // Inicia un timer para comprobar periódicamente si existen nuevos pedidos.
        public void InitTimer()
        {
            timerPedidos = new Timer();
            timerPedidos.Tick += new EventHandler(TimerPedidos_Tick);
            timerPedidos.Interval = 2000; // Intervalo en milisegundos
            timerPedidos.Start();
        }

        private void TimerPedidos_Tick(object sender, EventArgs e)
        {
            CambiaPedidoPlato();
        }

        private void NoListo_Click(object sender, RoutedEventArgs e)
        {
            // Si hay algún plato seleccionado, cambia su estado a no listo
            if (TablaPlatosPedidos.SelectedItem is null)
            {
                System.Windows.MessageBox.Show("No hay platos seleccionados", "Error");

            }
            else
            {

                string id = (TablaPlatosPedidos.SelectedItem as DataRowView).Row.ItemArray[0].ToString();
                conexion.CambiaEstadoPlato(id, 0);
                CambiaPedidoPlato();

            }
        }

        private void Listo_Click(object sender, RoutedEventArgs e)
        {
            // Si hay algún plato seleccionado, cambia su estado a listo y si ya no quedan platos
            // por preparar cambia el estado del pedido a completado y desaparece de la cola de 
            // pedidos pendientes

            if (TablaPlatosPedidos.SelectedItem is null)
            {
                System.Windows.MessageBox.Show("No hay platos seleccionados", "Error");

            }
            else
            {
                string id_plato = (TablaPlatosPedidos.SelectedItem as DataRowView).Row.ItemArray[0].ToString();
                conexion.CambiaEstadoPlato(id_plato, 1);
                if (!CambiaPedidoPlato())
                {
                    conexion.CambiaEstadoPedido(dtp.Rows[0][0].ToString());
                    CambiaPedidoPlato();
                }


            }
        }

        private void TablaPedidos_Loaded(object sender, RoutedEventArgs e)
        {
            InitTimer();
        }

        private void TablaPedidos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}