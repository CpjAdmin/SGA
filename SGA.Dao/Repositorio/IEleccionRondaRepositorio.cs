using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    public interface IEleccionRondaRepositorio
    {
        List<EleccionRonda> ListadoEleccionesR(EleccionRonda EleccionR);
        Resultado AgregarER(EleccionRonda EleccionR);
        Resultado ActualizarER(EleccionRonda EleccionR);
        Resultado EliminarER(EleccionRonda EleccionR);
    }
}
