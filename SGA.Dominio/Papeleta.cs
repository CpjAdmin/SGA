using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Papeleta
    {
        public int Id_eleccion { get; set; }
        public int Id_ronda { get; set; }
        public int Id_papeleta { get; set; }
        public int Id_delegado { get; set; }
        public int Contador_papeletas { get; set; }
        public int Orden { get; set; }
        public int Num_papeletas { get; set; }
        public int Id_voto_blanco { get; set; }
        public int Num_votos { get; set; }
        public string Nombre { get; set; }
        public string Color_card { get; set; }
        public string Color_background { get; set; }
        public string I_estado { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }

        public Papeleta()
        {
            Id_eleccion = 0;
            Id_ronda = 0;
            Id_papeleta = 0;
            Id_delegado = 0;
            Contador_papeletas = 0;
            Orden = 0;
            Num_papeletas = 0;
            Id_voto_blanco = 0;
            Num_votos = 0;
            Nombre = "";
            Color_card = "";
            Color_background = "";
            I_estado = "";
            Usuario = "";
            Terminal = "";
        }
    }
}
