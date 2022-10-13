using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Proceso
    {
        public int Id_proceso { get; set; }
        public string Login { get; set; }
        public string Nombre_usuario { get; set; }
        public string Pagina { get; set; }
        public string Descripcion { get; set; }
        public string Navegador { get; set; }
        public string Login_name { get; set; }
        public string Terminal_id { get; set; }
        public string Fecha_ejecucion { get; set; }


        public Proceso()
        {
            Id_proceso = 0;
            Login = "";
            Nombre_usuario = "";
            Pagina = "";
            Descripcion = "";
            Navegador = "";
            Login_name = "";
            Terminal_id = "";
            Fecha_ejecucion = "";

        }

        public Proceso(int Id_proceso, string Login, string Nombre_usuario, string Pagina, string Descripcion, string Navegador, string Login_name, string Terminal_id, string Fecha_ejecucion)
        {
            this.Id_proceso = Id_proceso;
            this.Login = Login;
            this.Nombre_usuario = Nombre_usuario;
            this.Pagina = Pagina;
            this.Descripcion = Descripcion;
            this.Navegador = Navegador;
            this.Login_name = Login_name;
            this.Terminal_id = Terminal_id;
            this.Fecha_ejecucion = Fecha_ejecucion;
        }
    }
}
