using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Pagina
    {

        public string Href { get; set; }
        public string Permisos { get; set; }
        public string Usuario { get; set; }
        public string Terminal { get; set; }

        public Pagina()
        {
            Href = "";
            Permisos = "";
            Usuario = "";
            Terminal = "";
        }

        public Pagina(string Href, string Permisos, string Usuario, string Terminal)
        {
            this.Href = Href;
            this.Permisos = Permisos;
            this.Usuario = Usuario;
            this.Terminal = Terminal;
        }
    }
}
