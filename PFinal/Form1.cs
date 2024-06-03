using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Org.BouncyCastle.Utilities;
using PFinal.Data;
using PFinal.Data.Columnas;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace PFinal
{
    public partial class Form1 : Form
    {
        private Conexionmysql mousepads;

        // Lista de objetos Columnas que contiene todos los registros
        List<Columnas> todos;

        // Cursor para navegar por los registros
        cursor cursor1 = new cursor();


        // Objeto Columnas para almacenar los detalles de un registro
        Columnas msp = new Columnas();


        public Form1()
        {
            mousepads = new Conexionmysql();
            InitializeComponent();
        }
        private void buttonconectar_Click(object sender, EventArgs e)
        {
           
        }

        // Boton para crear un nuevo registro
        private void buttoncrear_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas crear un nuevo registro?", "Confirmar Creación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Si el usuario confirma la creación, se procede
                if (result == DialogResult.Yes)
                {
                    // Crear un nuevo registro con los valores de los textbox
                    msp.Nombre = textBoxnombre.Text;
                    msp.Precio = decimal.Parse(textBoxprecio.Text);
                    msp.Durabilidad = textBoxdurabilidad.Text;
                    msp.Material = textBoxmaterial.Text;
                    msp.Velocidad_deslizamiento = textBoxdeslizamiento.Text;
                    msp.Ancho_cm = decimal.Parse(textBoxancho.Text);
                    msp.Largo_cm = decimal.Parse(textBoxlargo.Text);
                    mousepads.Crear(msp);

                    // Actualizar la lista de registros
                    List<Columnas> tod = mousepads.ObtenerTodosLosMousepads();
                    dataGridViewmousepad.DataSource = tod;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
            

        }

       

        private void buttonleer_Click(object sender, EventArgs e)
        {

            try
            {
                // Actualizar la lista de registros
                List<Columnas>tod = mousepads.ObtenerTodosLosMousepads();
            dataGridViewmousepad.DataSource = tod;

            todos = mousepads.ObtenerTodosLosMousepads();


                // Verificar si hay registros
                if (todos.Count > 0)
            {
                cursor1.totalRegistros = todos.Count;
                cursor1.current = 0;
                MostrarRegistro();
            }
            else
            {
                MessageBox.Show("No hay registros");
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }


        }

        // Boton para actualizar un registro
        private void buttonactualizar_Click(object sender, EventArgs e)
        {
            try
            {
                

                //confirmación del usuario antes de actualizar
                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas actualizar este registro?", "Confirmar Actualización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //Si el usuario confirma la actualización, se procede
                if (result == DialogResult.Yes)
                {
                    //Actualiza un registro con los valores de los textbox
                    msp.Id = int.Parse(textBoxID.Text);
                    msp.Nombre = textBoxnombre.Text;
                    msp.Precio = decimal.Parse(textBoxprecio.Text);
                    msp.Durabilidad = textBoxdurabilidad.Text;
                    msp.Material = textBoxmaterial.Text;
                    msp.Velocidad_deslizamiento = textBoxdeslizamiento.Text;
                    msp.Ancho_cm = decimal.Parse(textBoxancho.Text);
                    msp.Largo_cm = decimal.Parse(textBoxlargo.Text);
                    mousepads.actualizar(msp);

                    //Actualiza la lista de registros
                    List<Columnas> tod = mousepads.ObtenerTodosLosMousepads();
                    dataGridViewmousepad.DataSource = tod;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }



        }




        private void buttonID_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Busca un registro por ID y lo muestra en el datagridview
                DataTable dt = mousepads.BuscarPorId(int.Parse(textBoxID.Text));
                dataGridViewmousepad.DataSource = dt;
                
                //Validamos que existan datos en la fila
                if (dt.Rows.Count > 0)
                {
                    // Actualiza los campos de texto con la información del registro encontrado
                    DataRow row = dt.Rows[0]; //accede a la fila que se manda a traves del ID
                    textBoxID.Text = row["Id"].ToString();
                    textBoxnombre.Text = row["Nombre"].ToString();
                    textBoxprecio.Text = row["Precio"].ToString();
                    textBoxdurabilidad.Text = row["Durabilidad"].ToString();
                    textBoxmaterial.Text = row["Material"].ToString();
                    textBoxdeslizamiento.Text = row["Velocidad_deslizamiento"].ToString();
                    textBoxancho.Text = row["Ancho_cm"].ToString();
                    textBoxlargo.Text = row["Largo_cm"].ToString();
                }
                else
                {
                    MessageBox.Show("No se encontró ningún registro con el ID proporcionado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al encontrar el registro: " + ex.Message);
            }
        }



        // Boton para eliminar un registro
        private void buttoneliminar_Click(object sender, EventArgs e)
        {

            try {

                // Verifica si el ID es valido
                if (int.Parse(textBoxID.Text) > 0)
                {

                    if (MessageBox.Show("¿Estás seguro de que deseas eliminar el registro?", "Eliminar Registro", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        mousepads.EliminarRegistroPorId(int.Parse(textBoxID.Text));

                        // Actualiza la lista de registros
                        List<Columnas> tod = mousepads.ObtenerTodosLosMousepads();
                        dataGridViewmousepad.DataSource = tod;
                    }


                }
             

                
            
            }
            catch(Exception ex)
            { 
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
        }



        // Metodo para mostrar el registro anterior
        private void MostrarRegistroregresar()
        {

            if (cursor1.current >= 0 && cursor1.current < cursor1.totalRegistros)
            {
                // Obtener el registro actual desde la lista de registros
                Columnas msp = todos[cursor1.current];

                //Mostrar los detalles del registro en los textbox
                textBoxID.Text = msp.Id.ToString();
                textBoxnombre.Text = msp.Nombre;
                textBoxprecio.Text = msp.Precio.ToString();
                textBoxdurabilidad.Text = msp.Durabilidad;
                textBoxmaterial.Text = msp.Material;
                textBoxdeslizamiento.Text = msp.Velocidad_deslizamiento;
                textBoxancho.Text = msp.Ancho_cm.ToString();
                textBoxlargo.Text = msp.Largo_cm.ToString();

                // Mover el cursor al registro anterior
                cursor1.current--;

                // Verificar si se alcanzó el final de los registros
                if (cursor1.current >= cursor1.totalRegistros)
                {
                    cursor1.current = 0;
                    MessageBox.Show("Fin de los registros");
                }
            }
        }

        private void MostrarRegistro()
        {
            if (cursor1.current >= 0 && cursor1.current < cursor1.totalRegistros)
            {

                // Obtener el registro actual desde la lista de registros

                Columnas msp = todos[cursor1.current];


                // Mostrar los detalles del registro en los textbox
                textBoxID.Text = msp.Id.ToString();
                textBoxnombre.Text = msp.Nombre;
                textBoxprecio.Text = msp.Precio.ToString();
                textBoxdurabilidad.Text = msp.Durabilidad;
                textBoxmaterial.Text = msp.Material;
                textBoxdeslizamiento.Text = msp.Velocidad_deslizamiento;
                textBoxancho.Text = msp.Ancho_cm.ToString();
                textBoxlargo.Text = msp.Largo_cm.ToString() ;

                // Mover el cursor al siguiente registro
                cursor1.current++;

                // Verificar si se alcanzó el final de los registros
                if (cursor1.current >= cursor1.totalRegistros)
                {
                    cursor1.current = 0;
                    MessageBox.Show("Fin de los registros");
                }
            }
        }


        // Boton para mostrar el registro siguiente
        private void buttonsiguiente_Click(object sender, EventArgs e)
        {
            try { 

            MostrarRegistro();

                //// Actualizar la lista de registros
                List<Columnas> tod = mousepads.ObtenerTodosLosMousepads();
                dataGridViewmousepad.DataSource = tod;

            }
            catch(Exception ex) 


            { MessageBox.Show(ex.Message); }
            
        }


        // Boton para mostrar el registro anterior
        private void buttonregresar_Click(object sender, EventArgs e)
        {

            try {
               
                MostrarRegistroregresar();

                //Actualizar la lista de registros
                List<Columnas> tod = mousepads.ObtenerTodosLosMousepads();
                dataGridViewmousepad.DataSource = tod;

            }
            catch(Exception ex) 
            { MessageBox.Show(ex.ToString()); }
            
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
