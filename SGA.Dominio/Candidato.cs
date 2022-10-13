using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Candidato
    {
        public int Id_eleccion { get; set; }
        public int Id_ronda { get; set; }
        public int Id_papeleta { get; set; }
        public int Id_candidato { get; set; }
        public int Num_posicion { get; set; }
        public string Descripcion { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Foto { get; set; }
        public string I_estado { get; set; }
      

        public Candidato()
        {
            Id_eleccion = 0;
            Id_ronda = 0;
            Id_papeleta  = 0;
            Id_candidato = 0;
            Num_posicion = 0;
            Descripcion = "";
            Cedula = "";
            Nombre = "";
            Foto = "usuario.png";
            I_estado = "A";
        }

        public Candidato(int Id_eleccion, int Id_ronda, int Id_papeleta, int Id_candidato, int Num_posicion,string Descripcion, string Cedula,string Nombre,string Foto, string I_estado)
        {
            this.Id_eleccion = Id_eleccion ;
            this.Id_ronda = Id_ronda;
            this.Id_papeleta = Id_papeleta;
            this.Id_candidato = Id_candidato;
            this.Num_posicion = Num_posicion;
            this.Descripcion = Descripcion;
            this.Cedula = Cedula;
            this.Nombre = Nombre;
            this.Foto = Foto;
            this.I_estado = I_estado;
        }
    }
}
