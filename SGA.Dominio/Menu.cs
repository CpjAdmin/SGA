using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Menu
    {

        public string Modulo { get; set; }
        public string Submodulo { get; set; }
        public string Html_inicio_menu { get; set; }
        public string Html_menu { get; set; }
        public string Html_inicio_submenu { get; set; }
        public string Html_submenu { get; set; }


        public Menu()
        {
            Modulo = string.Empty;
            Submodulo = string.Empty;
            Html_inicio_menu = string.Empty;
            Html_menu = string.Empty;
            Html_inicio_submenu = string.Empty;
            Html_submenu = string.Empty;

        }

        public Menu(string Modulo, string Submodulo, string Html_inicio_menu, string Html_menu, string Html_inicio_submenu, string Html_submenu)
        {
            this.Modulo = Modulo;
            this.Submodulo = Submodulo;
            this.Html_inicio_menu = Html_inicio_menu;
            this.Html_menu = Html_menu;
            this.Html_inicio_submenu = Html_inicio_submenu;
            this.Html_submenu = Html_submenu;
        }
    }
}
