using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    public interface IBitacoraProcRepositorio
    {
        List<Proceso> ListadoProcesos();
        void RegistrarEvento(int idproceso, string login_name, string pagina, string descripcion, string terminal, string i_estado);
        void WriteToFile(string Message);
    }
}
