using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class ParametrosGenerales
    {
        public int Id_parametro { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string I_ejecucion { get; set; }
        public string I_estado { get; set; }

        public ParametrosGenerales()
        {
            Id_parametro = 0;
            Nombre = "";
            Descripcion = "";
            I_ejecucion = "";
            I_estado = "";
        }

        public ParametrosGenerales(int Id_parametro, string Nombre, string Descripcion, string I_ejecucion, string I_estado)
        {
            this.Id_parametro = Id_parametro;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.I_ejecucion = I_ejecucion;
            this.I_estado = I_estado;
        }
    }
}