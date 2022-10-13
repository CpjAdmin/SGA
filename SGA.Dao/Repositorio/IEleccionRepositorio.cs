using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    public interface IEleccionRepositorio
    {
        List<Eleccion> ListadoElecciones();
        Resultado AgregarE(Eleccion Eleccion);
        Resultado ActualizarE(Eleccion Eleccion);
        Resultado EliminarE(Eleccion Eleccion);
    }
}
