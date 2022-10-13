using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class PapeletaRonda
    {
        public int Id_eleccion { get; set; }
        public int Id_ronda { get; set; }
        public int Id_papeleta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Num_votos { get; set; }
        public int Orden { get; set; }
        public string I_estado { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }

        public PapeletaRonda()
        {
            Id_eleccion = 0;
            Id_ronda = 0;
            Id_papeleta = 0;
            Nombre = "";
            Descripcion = "";
            Num_votos = 0;
            Orden = 0;
            I_estado = "";
            Usuario = "";
            Terminal = "";
        }

        public PapeletaRonda(int Id_eleccion, int Id_ronda, int Id_papeleta, string Nombre, string Descripcion, int Num_votos, int Orden, string I_estado, string Usuario, string Terminal)
        {
            this.Id_eleccion = Id_eleccion;
            this.Id_ronda = Id_ronda;
            this.Id_papeleta = Id_papeleta;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Num_votos = Num_votos;
            this.Orden = Orden;
            this.I_estado = I_estado;
            this.Usuario = Usuario;
            this.Terminal = Terminal;
        }
    }
}
