using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Dispositivo
    {
        public int Id_dispositivo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public string I_estado { get; set; }
        public DateTime F_creacion { get; set; }
        public DateTime F_modifica { get; set; }
        public string User_creacion { get; set; }
        public string User_modifica { get; set; }
        public string Terminal_creacion { get; set; }
        public string Terminal_modifica { get; set; }

        public Dispositivo()
        {
            Id_dispositivo = 0;
            Nombre = "";
            Descripcion = "";
            Telefono = "";
            I_estado = "A";
            F_creacion = DateTime.Now;
            F_modifica = DateTime.Now;
            User_creacion = "";
            User_modifica = "";
            Terminal_creacion = "";
            Terminal_modifica = "";
        }

        public Dispositivo(int Id_dispositivo, string Nombre, string Descripcion, string Telefono, string I_estado, DateTime F_creacion, DateTime F_modifica, string User_creacion, string User_modifica, string Terminal_creacion, string Terminal_modifica)
        {
            this.Id_dispositivo= Id_dispositivo;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
            this.Telefono = Telefono;
            this.I_estado = I_estado;
            this.F_creacion = F_creacion;
            this.F_modifica = F_modifica;
            this.User_creacion = User_creacion;
            this.User_modifica = User_modifica;
            this.Terminal_creacion = Terminal_creacion;
            this.Terminal_modifica = Terminal_modifica;
        }
    }
}
