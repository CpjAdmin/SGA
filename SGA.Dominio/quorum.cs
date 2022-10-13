using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dominio
{
    public class Quorum
    {
        public int Id_delegado { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public int Num_paleta { get; set; }
        public string F_ing_sal { get; set; }
        public string Tipo { get; set; }


        public Quorum()
        {
            Id_delegado = 0;
            Cedula = "";
            Nombre = "";
            Num_paleta = 0;
            F_ing_sal = null;
            Tipo = "";
        }

        public Quorum(int Id_delegado, string Cedula, string Nombre, int Num_paleta, string F_ing_sal, string Tipo)
        {
            this.Id_delegado = Id_delegado;
            this.Cedula = Cedula;
            this.Nombre = Nombre;
            this.Num_paleta = Num_paleta;
            this.F_ing_sal = F_ing_sal;
            this.Tipo = Tipo;
        }
    }
}
