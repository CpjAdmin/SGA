using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class ResultadoEleccion
    {
        public int Id_eleccion { get; set; }
        public int Id_ronda { get; set; }
        public int Id_papeleta { get; set; }
        public int Id_candidato { get; set; }
        public int Num_posicion { get; set; }
        public string Papeleta { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Foto { get; set; }
        public int Votos { get; set; }
        public int Posicion { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }

        public ResultadoEleccion()
        {
            Id_eleccion = 0;
            Id_ronda = 0;
            Id_papeleta = 0;
            Id_candidato = 0;
            Num_posicion = 0;
            Papeleta = "";
            Cedula = "";
            Nombre = "";
            Foto = "";
            Votos = 0;
            Posicion = 0;
            Usuario = "";
            Terminal = "";
        }

    }
}
