using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PFinal.Data.Columnas
{
    internal class Columnas
    {   
        
        
        //Estos son los parametros que se van a usar en nuestros metodos para mostrar y enviar la informacion
        public int Id {  get; set; }
        public string Nombre {  get; set; }
        public decimal Precio { get; set; }
        public string Durabilidad { get; set; }
        public string Material {  get; set; }
        public string Velocidad_deslizamiento { get; set; }
        public decimal Ancho_cm { get; set; }
        public decimal Largo_cm { get; set; }
            
        //constructor vacio
        public Columnas()
        {

        }
        
        
        //Constructor con parametros
        public Columnas(int id, string nombre, decimal precio, string durabilidad, string material, string velocidad_deslizamiento, decimal ancho_cm, decimal largo_cm)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Precio = precio;
            this.Durabilidad = durabilidad;
            this.Material = material;
            this.Velocidad_deslizamiento = velocidad_deslizamiento;
            this.Ancho_cm = ancho_cm;
            this.Largo_cm = largo_cm;
        }



        


    }
}
