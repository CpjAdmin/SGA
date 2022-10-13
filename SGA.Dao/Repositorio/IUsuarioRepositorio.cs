using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.Dominio;

namespace SGA.Dao.Repositorio
{
    public interface IUsuarioRepositorio
    {
        int ValidarU(string Login, string Token, string Clave, string navegador);
        Usuario Buscar(string Login, string Clave);
        Usuario BuscarT(string cadena);
        Resultado InsertarU(Usuario objUsuario);
        Resultado EliminarU(Usuario objUsuario);
        bool ActualizaP(string Login, string Pin, string usuario, string terminal);
        Usuario BuscarL(string Login);
        Usuario BuscarD(string Login);
        Usuario BuscarDI(string Login);
        Resultado EnviarPin(string telefono, string pin, string login, string usuario,string tipo);
        Resultado EnviarPinD(string telefono, string pin, string login,string nombre, string usuario, string tipo);
        List<Usuario> ListaUsuarios();
        List<Rol> ListaRoles();
        ParametrosGenerales BuscarParam(string nomParam);
        void InsertarBitSMS(string usuario, string login, string telefono, string status, string statustext,string responsetext,string tipo);
        int ObtenerRol(string login);
        List<Menu> CargarMenu(string login, string terminal);
        Pagina ValidarPermisosPagina(Pagina objPagina);

    }
}
