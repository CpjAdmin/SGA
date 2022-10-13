using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Rol
    {
        public int Id_rol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string I_estado { get; set; }
        public DateTime F_creacion { get; set; }
        public DateTime F_modifica { get; set; }
        public string User_creacion { get; set; }
        public string User_modifica { get; set; }
        public string Terminal_creacion { get; set; }
        public string Terminal_modifica { get; set; }

        public Rol()
        {
            Id_rol = 0;
            Nombre = "";
            Descripcion = "";
            I_estado = "";
            F_creacion = DateTime.Now;
            F_modifica = DateTime.Now;
            User_creacion = "";
            User_modifica = "";
            Terminal_creacion = "";
            Terminal_modifica = "";
        }

        public Rol(int Id_rol, string Nombre, string Descripcion, string I_estado, DateTime F_creacion, DateTime F_modifica, string User_creacion, string User_modifica, string Terminal_creacion, string Terminal_modifica)
        {
            this.Id_rol = Id_rol;
            this.Nombre = Nombre;
            this.Descripcion = Descripcion;
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
