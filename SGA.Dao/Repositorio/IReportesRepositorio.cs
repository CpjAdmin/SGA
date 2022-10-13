using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.Dominio;

namespace SGA.Dao.Repositorio
{
    public interface IReportesRepositorio
    {
        List<Reporte> ListadoReportes(string usuario);
    }
}
