using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Usuario
    {
       
        public int Id_usuario { get; set; }
        public int Id_delegado { get; set; }
        public string Login { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Token { get; set; }
        public string Telefono { get; set; }
        public int Sesion_act { get; set; }
        public int Sesion_max { get; set; }
        public string Foto { get; set; }
        public string I_delegado { get; set; }
        public string I_estado { get; set; }
        public DateTime F_ult_ingreso { get; set; }
        public DateTime F_creacion { get; set; }
        public DateTime F_modifica { get; set; }
        public string User_creacion { get; set; }
        public string User_modifica { get; set; }
        public string Terminal_creacion { get; set; }
        public string Terminal_modifica { get; set; }
        public string Navegador { get; set; }

        public int Id_rol { get; set; }
        public string Nombre_rol { get; set; }
        public string Pagina_ref { get; set; }
      
       
        

        public Usuario()
        {
            Id_usuario = 0;
            Id_delegado = 0;
            Login = "";
            Nombre = "";
            Clave = "";
            Token = "";
            Telefono = "";
            Sesion_act = 0;
            Sesion_max = 1;
            Foto = "usuario.png";
            I_delegado = "";
            I_estado = "A";
            F_ult_ingreso = DateTime.Now;
            F_creacion = DateTime.Now;
            F_modifica = DateTime.Now;
            User_creacion = "";
            User_modifica = "";
            Terminal_creacion = "";
            Terminal_modifica = "";
            Navegador = "";

            Id_rol = 0;
            Nombre_rol = "";
            Pagina_ref = "";
          
        }

        public Usuario(int Id_usuario, int Id_delegado, string Login, string Nombre, string Clave, string Token, string Telefono, int Sesion_act, int Sesion_max, string Foto, string I_delegado, string I_estado, DateTime F_ult_ingreso
            , DateTime F_creacion, DateTime F_modifica, string User_creacion, string User_modifica, string Terminal_creacion, string Terminal_modifica, string Navegador, int Id_rol, string Nombre_rol, string Pagina_ref)
        {
            this.Id_usuario = Id_usuario;
            this.Id_delegado = Id_delegado;
            this.Login = Login;
            this.Nombre = Nombre;
            this.Clave = Clave;
            this.Token = Token;
            this.Telefono = Telefono;
            this.Sesion_act = Sesion_act;
            this.Sesion_max = Sesion_max;
            this.Foto = Foto;
            this.I_delegado = I_delegado;
            this.I_estado = I_estado;
            this.F_ult_ingreso = F_ult_ingreso;
            this.F_creacion = F_creacion;
            this.F_modifica = F_modifica;
            this.User_creacion = User_creacion;
            this.User_modifica = User_modifica;
            this.Terminal_creacion = Terminal_creacion;
            this.Terminal_modifica = Terminal_modifica;
            this.Navegador = Navegador;

            this.Id_rol = Id_rol;
            this.Nombre_rol = Nombre_rol;
            this.Pagina_ref = Pagina_ref;
        }
    }
}

