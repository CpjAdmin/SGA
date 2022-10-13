using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Delegado
    {
        public int Id_delegado { get; set; }
        public string Cedula { get; set; }
        public int Num_paleta { get; set; }
        public string Nombre { get; set; }
        public string Institucion { get; set; }
        public string Centro { get; set; }
        public string Lugar_Trabajo { get; set; }
        public string Tel_Celular { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public string I_estado { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }
        public string I_votar { get; set; }

        public Delegado()
        {
            Id_delegado = 0;
            Cedula = "";
            Num_paleta = 0;
            Nombre = "";
            Institucion = "";
            Centro = "";
            Lugar_Trabajo = "";
            Tel_Celular = "";
            Email = "";
            Foto = "";
            I_estado = "";
            Usuario = "";
            Terminal = "";
            I_votar = "S";
        }

        public Delegado(int Id_delegado, string Cedula, int Num_paleta, string Nombre, string Institucion, string Centro, string Lugar_Trabajo, string Tel_Celular, string Email, string Foto, string I_estado, string Usuario, string Terminal,string I_votar)
        {
            this.Id_delegado = Id_delegado;
            this.Cedula = Cedula;
            this.Num_paleta = Num_paleta;
            this.Nombre = Nombre;
            this.Institucion = Institucion;
            this.Centro = Centro;
            this.Lugar_Trabajo = Lugar_Trabajo;
            this.Tel_Celular = Tel_Celular;
            this.Email = Email;
            this.Foto = Foto;
            this.I_estado = I_estado;
            this.Usuario = Usuario;
            this.Terminal = Terminal;
            this.I_votar = I_votar;
        }

    }
}
