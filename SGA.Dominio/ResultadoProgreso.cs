using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class ResultadoProgreso
    {
        public int Id_eleccion { get; set; }
        public int Id_ronda { get; set; }
        public int Id_delegado { get; set; }
        public string Login { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public int Papeletas { get; set; }
        public int Votos { get; set; }
        public string F_inicio_voto { get; set; }
        public string F_fin_voto { get; set; }
        public string Duracion { get; set; }
        public string Progreso { get; set; }
        public int Sesion_act { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }

        public ResultadoProgreso()
        {
            Id_eleccion = 0;
            Id_ronda = 0;
            Id_delegado = 0;
            Login = "";
            Nombre = "";
            Telefono = "";
            Papeletas = 0;
            Votos = 0;
            F_inicio_voto = "";
            F_fin_voto = "";
            Duracion = "0";
            Progreso = "*";
            Sesion_act = 0;
            Usuario = "";
            Terminal = "";
        }
    }
}
