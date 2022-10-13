using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Gestion
    {
        public int Orden { get; set; }
        public int Num_paleta { get; set; }
        public int Num_posicion { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Tiempo { get; set; }
        public string Terminal { get; set; }
        public string Reincide { get; set; }

        public Gestion()
        {
            Orden = 0;
            Num_paleta = 0;
            Num_posicion = 0;
            Cedula = "";
            Nombre = "";
            Tiempo = "";
            Usuario = "";
            Terminal = "";
            Reincide = "N";
        }

        public Gestion(int orden, int Num_paleta, int Num_posicion, string Cedula, string Nombre, string tiempo, string Usuario, string Terminal, string Reincide)
        {
            this.Orden = orden;
            this.Num_paleta = Num_paleta;
            this.Num_posicion = Num_posicion;
            this.Cedula = Cedula;
            this.Nombre = Nombre;
            this.Usuario = Usuario;
            this.Terminal = Terminal;
            this.Tiempo = Tiempo;
            this.Reincide = Reincide;
        }

    }
}