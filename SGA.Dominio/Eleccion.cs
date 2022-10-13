using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Eleccion
    {
        public int Id_eleccion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string F_inicio { get; set; }
        public string F_final { get; set; }
        public string I_estado { get; set; }
        public string I_cierre_quorum { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }


        public Eleccion()
        {
            Id_eleccion = 0;
            Nombre = "";
            Descripcion = "";
            F_inicio = "";
            F_final = "";
            I_estado = "";
            I_cierre_quorum = "";
            Usuario = "";
            Terminal = "";
        }

        public Eleccion(int Id_eleccion, string Nombre, string Descripcion , string F_inicio, string F_final, string I_estado, string I_cierre_quorum, string Usuario, string Terminal)
        {
            this.Id_eleccion = Id_eleccion;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.F_inicio = F_inicio;
            this.F_final = F_final;
            this.I_estado = I_estado;
            this.I_cierre_quorum = I_cierre_quorum;
            this.Usuario = Usuario;
            this.Terminal = Terminal;
        }
    }
}
