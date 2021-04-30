using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Proyecto_I
{
    public class Conexiones
    {
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
                server = "remotemysql.com";
                database = "QARfHL3dKT";
                uid = "QARfHL3dKT";
                password = "l1Un76lQq7";
                //server = "order-deliver.cdizvox6kcwq.eu-west-3.rds.amazonaws.com";
                //database = "OD";
                //uid = "admin";
                //password = "12345678";
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
            MySqlConnection conexion = Initialize();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM clientes", conexion);
            conexion.Open();
            DataTable dt = new DataTable();

            dt.Load(cmd.ExecuteReader());

            conexion.Close();

            return dt;
        }

        public bool InsertaCliente(string nombre, string apellidos, string alias, string pass, string correo, string rol)
        {
            MySqlConnection conexion = Initialize();
            string cmdText = "INSERT INTO clientes(nombre, apellidos, alias, pass, correo, rol) values (@nombre, @apellidos, @alias, @pass, @email, @rol);";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@apellidos", apellidos);
            cmd.Parameters.AddWithValue("@alias", alias);
            cmd.Parameters.AddWithValue("@pass", pass);
            cmd.Parameters.AddWithValue("@email", correo);
            cmd.Parameters.AddWithValue("@rol", rol);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("El usuario " + alias + " ha sido añadido.", "Completado");

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo añadir el usuario", "Error");
                Console.WriteLine(e.StackTrace);
                return false;
            }

            conexion.Close();
            return true;
        }


        public bool ModificaCliente(string id, string nombre, string apellidos, string alias, string pass, string correo, string rol)
        {
            MySqlConnection conexion = Initialize();

            string cmdText = "UPDATE clientes SET nombre=@nombre, apellidos=@apellidos, alias=@alias, pass=@pass, correo=@correo, rol=@rol WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@apellidos", apellidos);
            cmd.Parameters.AddWithValue("@alias", alias);
            cmd.Parameters.AddWithValue("@pass", pass);
            cmd.Parameters.AddWithValue("@correo", correo);
            cmd.Parameters.AddWithValue("@rol", rol);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("El usuario " + alias + " ha sido actualizado.", "Completado");

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo actualizar el usuario", "Error");
                Console.WriteLine(e.StackTrace);
                return false;
            }

            conexion.Close();
            return true;
        }

        public bool EliminaCliente(string id, string alias)
        {
            MySqlConnection conexion = Initialize();

            string cmdText = "DELETE FROM clientes WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);

            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("El usuario " + alias + " ha sido eliminado.", "Completado");

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo eliminar el usuario", "Error");
                Console.WriteLine(e.StackTrace);
                return false;
            }

            conexion.Close();
            return true;
        }


        //RESTAURANTE
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        public DataTable EstadoCocina()
        {
            try
            {
                MySqlConnection conexion = Initialize();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM pedidos", conexion);
                conexion.Open();
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());

                conexion.Close();

                return dt;
            }
            catch (Exception e)
            {

                System.Windows.MessageBox.Show("No se pudo buscar en platos", "Error");
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }
        public DataTable Menu()
        {
            try
            {
                MySqlConnection conexion = Initialize();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM platos", conexion);
                conexion.Open();
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());

                conexion.Close();

                return dt;
            }
            catch (Exception e)
            {

                System.Windows.MessageBox.Show("No se pudo buscar en platos", "Error");
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }


        public bool InsertaPlato(string nombre, bool? disponible, double precio)
        {
            try
            {
                MySqlConnection conexion = Initialize();
                string cmdText = "INSERT INTO platos(nombre, disponible, precio) values (@nombre, @disponible, @precio);";
                MySqlCommand cmd = new MySqlCommand(cmdText, conexion);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@disponible", disponible);
                cmd.Parameters.AddWithValue("@precio", precio);

                conexion.Open();
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("El plato " + nombre + " ha sido insertado.", "Completado");

            }
            catch (Exception e)
            {

                System.Windows.MessageBox.Show("No se pudo Insertar el plato", "Error");
                Console.WriteLine(e.StackTrace);
                return false;
            }
            return true;
        }


        public bool ModificaPlato(string id, string nombre, bool? disponible, double precio)
        {
            MySqlConnection conexion = Initialize();

            string cmdText = "UPDATE platos SET nombre=@nombre, disponible=@disponible, precio=@precio WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@disponible", disponible);
            cmd.Parameters.AddWithValue("@precio", precio);


            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("El plato " + nombre + " ha sido actualizado.", "Completado");

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo actualizar el plato", "Error");
                Console.WriteLine(e.StackTrace);
                return false;
            }

            conexion.Close();
            return true;
        }

        public bool EliminaPlato(string id, string nombre)
        {
            MySqlConnection conexion = Initialize();

            string cmdText = "DELETE FROM platos WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);

            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();
                System.Windows.MessageBox.Show("El plato " + nombre + " ha sido eliminado.", "Completado");

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo eliminar el plato", "Error");
                Console.WriteLine(e.StackTrace);
                return false;
            }

            conexion.Close();
            return true;
        }

        //LOGIN
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DataTable ComprobarLogin(string aliasCorreo, string contrasena)
        {
            try
            {


                DataTable dt = new DataTable();
                string resultado = string.Empty;
                MySqlConnection conexion = Initialize();
                MySqlCommand cmd = new MySqlCommand();
                if (!aliasCorreo.Contains('@'))
                {
                    string cmdText = "SELECT * FROM clientes WHERE alias = @alias and pass = @password;";
                    cmd = new MySqlCommand(cmdText, conexion);

                    cmd.Parameters.AddWithValue("@alias", aliasCorreo);
                    cmd.Parameters.AddWithValue("@password", contrasena);
                }
                else
                {
                    string cmdText = "SELECT * FROM clientes WHERE correo = @correo and pass = @password;";
                    cmd = new MySqlCommand(cmdText, conexion);

                    cmd.Parameters.AddWithValue("@correo", aliasCorreo);
                    cmd.Parameters.AddWithValue("@password", contrasena);
                }


                conexion.Open();
                dt.Load(cmd.ExecuteReader());


                return dt;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se comprobar el usuario", "Error");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }


        //CLIENTES
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DataTable Carta()
        {
            try
            {
                DataTable dt = new DataTable();
                string resultado = string.Empty;
                MySqlConnection conexion = Initialize();
                MySqlCommand cmd = new MySqlCommand();


                string cmdText = "SELECT * FROM platos WHERE disponible = 1;";
                cmd = new MySqlCommand(cmdText, conexion);

                conexion.Open();
                dt.Load(cmd.ExecuteReader());

                return dt;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se comprobar el usuario", "Error");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public bool InsertaPedido(string id_cliente, DataTable dt)
        {
            MySqlConnection conexion = Initialize();
            string cmdText = "INSERT INTO pedidos(id_cliente, direccion) values (@id_cliente, 'Calle de los laureles, 13, 28032, Madrid'); ";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);

            cmd.Parameters.AddWithValue("@id_cliente", id_cliente);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo tramitar el pedido", "Error");
                Console.WriteLine(e.StackTrace);
                return false;
            }

            // Inserto los platos
            string id_pedido = DevuelveIdPedido();

            // Voy añadiendo los platos uno a uno
            foreach (DataRow row in dt.Rows)
            {
                string cmdText2 = "INSERT INTO pedidos_platos(id_pedido, id_plato) values (@id, @id_plato); ";
                MySqlCommand cmd2 = new MySqlCommand(cmdText2, conexion);
                cmd2.Parameters.AddWithValue("@id", id_pedido);
                cmd2.Parameters.AddWithValue("@id_plato", row.ItemArray[0].ToString());

                try
                {
                    cmd2.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("No se pudo añadir el plato", "Error");
                    Console.WriteLine(e.StackTrace);
                    return false;
                }
            }



            conexion.Close();
            return true;
        }

        private string DevuelveIdPedido()
        {
            try
            {
                MySqlConnection conexion = Initialize();
                MySqlCommand cmd = new MySqlCommand("SELECT MAX(id) FROM pedidos", conexion);
                conexion.Open();
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());

                conexion.Close();

                return dt.Rows[0].ItemArray[0].ToString();
            }
            catch (Exception e)
            {

                System.Windows.MessageBox.Show("No se pudo buscar en platos", "Error");
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        /////// COCINA

        public DataTable Pedidos()
        {
            MySqlConnection conexion = Initialize();
            MySqlCommand cmd = new MySqlCommand("SELECT p.id AS Id, c.alias AS Alias_Cliente, TIME_FORMAT(p.fecha, '%H:%i') AS Hora_pedido FROM pedidos p, clientes c WHERE p.id_cliente=c.id AND completado=0;", conexion);

            conexion.Open();
            DataTable dt = new DataTable();

            dt.Load(cmd.ExecuteReader());

            conexion.Close();

            return dt;
        }

        public DataTable Platos()
        {
            MySqlConnection conexion = Initialize();
            MySqlCommand cmd = new MySqlCommand("SELECT p.id AS Id, c.alias AS Alias_Cliente, p.fecha AS Fecha FROM pedidos p, clientes c WHERE p.id_cliente=c.id;", conexion);

            conexion.Open();
            DataTable dt = new DataTable();

            dt.Load(cmd.ExecuteReader());

            conexion.Close();

            return dt;
        }

        public DataTable PedidosPlatos(string id_pedido)
        {
            MySqlConnection conexion = Initialize();
            MySqlCommand cmd = new MySqlCommand("SELECT pp.id, pp.id_pedido, p.nombre AS nombrePlat, pp.hecho  FROM pedidos_platos pp, platos p WHERE pp.id_pedido = @id_pedido AND pp.id_plato = p.id ORDER BY pp.hecho;", conexion);

            cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
            conexion.Open();
            DataTable dt = new DataTable();

            try
            {
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception e)
            {

                throw e;
            }

            conexion.Close();

            return dt;
        }

        public bool CambiaEstadoPlato(string id, int estado)
        {
            MySqlConnection conexion = Initialize();

            string cmdText = "UPDATE pedidos_platos SET hecho=@hecho WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@hecho", estado);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo actualizar el plato", "Error");
                Console.WriteLine(e.StackTrace);
                return false;
            }

            conexion.Close();
            return true;
        }

        public void CambiaEstadoPedido(string id)
        {
            MySqlConnection conexion = Initialize();

            string cmdText = "UPDATE pedidos SET completado=1 WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);

            cmd.Parameters.AddWithValue("@id", id);

            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo actualizar el pedido", "Error");
                Console.WriteLine(e.StackTrace);

            }

            conexion.Close();

        }
        /// <summary>
        /// REGISTRO
        /// </summary>
        /// <returns></returns>
        public bool ComprobacionCorreo(string correo)
        {
            bool result = false;
            MySqlConnection conexion = Initialize();

            string cmdText = "SELECT * FROM clientes WHERE correo = @correo;";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);

            cmd.Parameters.AddWithValue("@correo", correo);

            try
            {
                conexion.Open();

                if (cmd.ExecuteReader().HasRows)
                {
                    result = true;
                }
              

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo actualizar el pedido", "Error");
                Console.WriteLine(e.StackTrace);

            }

            conexion.Close();


            return result;
        }

        public bool ComprobacionAlias(string alias)
        {
            bool result = false;
            MySqlConnection conexion = Initialize();

            string cmdText = "SELECT * FROM clientes WHERE alias = @alias;";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);

            cmd.Parameters.AddWithValue("@alias", alias);

            try
            {
                conexion.Open();

                if (cmd.ExecuteReader().HasRows)
                {
                    result = true;
                }


            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo actualizar el pedido", "Error");
                Console.WriteLine(e.StackTrace);

            }

            conexion.Close();


            return result;
        }

        public bool InsertaCliente(string nombre, string apellidos, string alias, string correo,string contrasena)
        {
            MySqlConnection conexion = Initialize();
            string cmdText = "INSERT INTO clientes(nombre,apellidos,alias,pass,correo,rol) values (@nombre,@apellidos,@alias,@pass,@correo,@rol); ";
            MySqlCommand cmd = new MySqlCommand(cmdText, conexion);

            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@apellidos", apellidos);
            cmd.Parameters.AddWithValue("@alias", alias);
            cmd.Parameters.AddWithValue("@pass", contrasena);
            cmd.Parameters.AddWithValue("@correo", correo);
            cmd.Parameters.AddWithValue("@rol", "2");




            try
            {
                conexion.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("No se pudo insertar al cliente", "Error");
                Console.WriteLine(e.StackTrace);
                return false;
            }

           
            



            conexion.Close();
            return true;
        }


    }
}


