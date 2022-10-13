using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class PapeletaCandidato
    {
        public int Id_eleccion { get; set; }
        public int Id_ronda { get; set; }
        public int Id_papeleta { get; set; }
        public int Id_candidato { get; set; }
        public int Num_posicion { get; set; }
        public String Cedula { get; set; }
        public String Nombre { get; set; }
        public String Foto { get; set; }
        public String Descripcion { get; set; }
        public String I_estado { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }



        public PapeletaCandidato()
        {
            Id_eleccion = 0;
            Id_ronda = 0;
            Id_papeleta  = 0;
            Id_candidato = 0;
            Num_posicion = 0;
            Cedula = "";
            Nombre = "";
            Foto = "usuario.png";
            Descripcion = "";
            I_estado = "I";
            Usuario = "";
            Terminal = "";
        }

        public PapeletaCandidato(int Id_eleccion, int Id_ronda, int Id_papeleta, int Id_candidato, int Num_posicion, string Cedula,string Nombre,string Foto, string Descripcion, string I_estado, string Usuario, string Terminal)
        {
            this.Id_eleccion = Id_eleccion;
            this.Id_ronda = Id_ronda;
            this.Id_papeleta = Id_papeleta;
            this.Id_candidato = Id_candidato;
            this.Num_posicion = Num_posicion;
            this.Cedula = Cedula;
            this.Nombre = Nombre;
            this.Foto = Foto;
            this.Descripcion = Descripcion;
            this.I_estado = I_estado;
            this.Usuario = Usuario;
            this.Terminal = Terminal;
        }
    }
}
