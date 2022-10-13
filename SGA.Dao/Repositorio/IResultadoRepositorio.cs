using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    public interface IResultadoRepositorio
    {
        List<ResultadoEleccion> ResultadoResumen(ResultadoEleccion resultado);
        List<ResultadoProgreso> ResultadoProgreso(ResultadoProgreso resultado);
    }
}
