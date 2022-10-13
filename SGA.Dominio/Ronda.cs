using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Ronda
    {
        public int Id_ronda{ get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String I_estado { get; set; }

        public Ronda()
        {
            Id_ronda = 0;
            Nombre = "";
            Descripcion = "";
            I_estado = "";
        }
        public Ronda(int Id_ronda, string Nombre, string Descripcion,string I_estado)
        {
            this.Id_ronda = Id_ronda;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.I_estado = I_estado;
        }
    }
}
