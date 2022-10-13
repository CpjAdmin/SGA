using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class EleccionRonda
    {
        public int Id_eleccion { get; set; }
        public int Id_ronda { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string F_inicio { get; set; }
        public string F_final { get; set; }
        public string I_estado { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }

        public EleccionRonda()
        {
            Id_eleccion = 0;
            Id_ronda = 0;
            Nombre = "";
            Descripcion = "";
            F_inicio = "";
            F_final = "";
            I_estado = "";
            Usuario = "";
            Terminal = "";
        }

        public EleccionRonda(int Id_eleccion, int Id_ronda, string Nombre, string Descripcion, string F_inicio, string F_final, string I_estado, string Usuario, string Terminal)
        {
            this.Id_eleccion = Id_eleccion;
            this.Id_ronda = Id_ronda;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.F_inicio = F_inicio;
            this.F_final = F_final;
            this.I_estado = I_estado;
            this.Usuario = Usuario;
            this.Terminal = Terminal;
        }
    }
}
