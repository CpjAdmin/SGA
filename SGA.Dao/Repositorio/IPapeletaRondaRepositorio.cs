using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    public interface IPapeletaRondaRepositorio
    {
        List<PapeletaRonda> ListadoPapeletasR(PapeletaRonda PapeletaR);
        Resultado AgregarPR(PapeletaRonda PapeletaR);
        Resultado ActualizarPR(PapeletaRonda PapeletaR);
        Resultado EliminarPR(PapeletaRonda PapeletaR);
    }
}
