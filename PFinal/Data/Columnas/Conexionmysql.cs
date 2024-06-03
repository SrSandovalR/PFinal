using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Management;

namespace PFinal.Data.Columnas
{
    internal class Conexionmysql
    {

        private string connectionstring = "Server=LocalHost;Database=mouse_pad;uid=root;Pwd=1212ABCD0909aa";
        MySqlConnection connection;


        public Conexionmysql()
        {
            connection = new MySqlConnection(connectionstring);
        }


      



        //Método para crear un nuevo personaje

        public void Crear(Columnas msp)
        {
            try
            {
                string query = "INSERT INTO mousepads (nombre, precio, material, durabilidad, velocidad_deslizamiento, ancho_cm, largo_cm) VALUES (@nombre, @precio, @material, @durabilidad, @velocidad_deslizamiento, @ancho_cm, @largo_cm)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nombre", msp.Nombre);
                cmd.Parameters.AddWithValue("@precio", msp.Precio);
                cmd.Parameters.AddWithValue("@material", msp.Material);
                cmd.Parameters.AddWithValue("@durabilidad", msp.Durabilidad);
                cmd.Parameters.AddWithValue("@velocidad_deslizamiento", msp.Velocidad_deslizamiento);
                cmd.Parameters.AddWithValue("@ancho_cm", msp.Ancho_cm);
                cmd.Parameters.AddWithValue("@largo_cm", msp.Largo_cm);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            if (msp != null) { MessageBox.Show("El Registro Se Agrego Correctamente."); }
        }



        //metodo para actualizar personajes
       
        public void actualizar(Columnas msp)
        {
            try
            {

                string query = "UPDATE mousepads SET nombre = @nombre, precio = @precio, material = @material, durabilidad = @durabilidad, velocidad_deslizamiento = @velocidad_deslizamiento, ancho_cm = @ancho_cm, largo_cm = @largo_cm WHERE ID = @ID";      
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", msp.Id);
                cmd.Parameters.AddWithValue("@nombre", msp.Nombre);
                cmd.Parameters.AddWithValue("@precio", msp.Precio);
                cmd.Parameters.AddWithValue("@material", msp.Material);
                cmd.Parameters.AddWithValue("@durabilidad", msp.Durabilidad);
                cmd.Parameters.AddWithValue("@velocidad_deslizamiento", msp.Velocidad_deslizamiento);
                cmd.Parameters.AddWithValue("@ancho_cm", msp.Ancho_cm);
                cmd.Parameters.AddWithValue("@largo_cm", msp.Largo_cm);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
           

            if (msp != null)
            {
                MessageBox.Show("El Registro Se Actualizo Correctamente.");
            }
          
            
            
                
            
        }




        //Metodo para buscar por ID
        public DataTable BuscarPorId(int id)
        {
            DataTable mousepads = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionstring))
            {
                connection.Open();

                string sql = "SELECT * FROM mousepads WHERE ID = @ID";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(mousepads);
                    }
                }
            }

            return mousepads;
        }


        //Metodo para Eliminar un registro por ID
        public void EliminarRegistroPorId(int id)
        {
            try
            {

                using (MySqlConnection connection = new MySqlConnection(connectionstring))
                {
                    connection.Open();

                    string sql = "DELETE FROM mousepads WHERE ID = @ID";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        int filaeliminada = command.ExecuteNonQuery();
                        
                        if (filaeliminada > 0)
                        {
                            MessageBox.Show("El Registro Se Elimino Correctamente.");
                        }
                           
                        

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }


        }

        //Metodo para Obtener las listas
        public List<Columnas> ObtenerTodosLosMousepads()
        {
            List<Columnas> mousepads = new List<Columnas>();

            using (MySqlConnection connection = new MySqlConnection(connectionstring))
            {
                string query = "SELECT * FROM mousepads ";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Columnas mousepad = new Columnas
                        (
                            id: reader.GetInt32("ID"),
                            nombre: reader.GetString("nombre"),
                            precio: reader.GetDecimal("precio"),
                            durabilidad: reader.GetString("durabilidad"),
                            material: reader.GetString("material"),
                            velocidad_deslizamiento: reader.GetString("velocidad_deslizamiento"),
                            ancho_cm: reader.GetDecimal("ancho_cm"),
                            largo_cm: reader.GetDecimal("largo_cm")

                        );

                        mousepads.Add(mousepad);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return mousepads;
        }



    }
}
