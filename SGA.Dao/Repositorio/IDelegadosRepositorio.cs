using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    public interface IDelegadosRepositorio
    {
        Resultado Insertar(Delegado objDelegado);
        Resultado Actualizar(Delegado objDelegado);
        Resultado Eliminar(int id);
        Delegado BuscarId(Delegado objDelegado);
        List<Delegado> ListaDelegados();
        Resultado Trasladar(Delegado objDelegado);
        int TotalDelegados();
    }
}
