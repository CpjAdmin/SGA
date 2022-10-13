using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Voto
    {
        public int Id_delegado { get; set; }
        public int Id_eleccion { get; set; }
        public int Id_ronda { get; set; }
        public int Id_papeleta { get; set; }
        public int Id_voto { get; set; }
        public int Id_candidato { get; set; }
        public string I_estado { get; set; }
        public string F_creacion { get; set; }
        //Atributos Adicionales
        public string Lista_candidatos { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }

        public Voto()
        {
            Id_delegado = 0;
            Id_eleccion = 0;
            Id_ronda = 0;
            Id_papeleta = 0;
            Id_voto = 0;
            Id_candidato = 0;
            I_estado = "";
            F_creacion = "";
            Lista_candidatos = "";
            Usuario = "";
            Terminal = "";
        }

        public Voto(int Id_delegado, int Id_eleccion, int Id_ronda, int Id_papeleta, int Id_voto,
                    int Id_candidato, string I_estado, string F_creacion, string Lista_candidatos,
                    string Usuario, string Terminal)
        {
            this.Id_delegado = Id_delegado;
            this.Id_eleccion = Id_eleccion;
            this.Id_ronda = Id_ronda;
            this.Id_papeleta = Id_papeleta;
            this.Id_voto = Id_voto;
            this.Id_candidato = Id_candidato;
            this.I_estado = I_estado;
            this.F_creacion = F_creacion;
            this.Lista_candidatos = Lista_candidatos;
            this.Usuario = Usuario;
            this.Terminal = Terminal;

        }
    }
}
