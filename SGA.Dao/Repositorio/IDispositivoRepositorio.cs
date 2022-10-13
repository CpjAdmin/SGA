using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGA.Dominio;

namespace SGA.Dao.Repositorio
{
    public interface IDispositivoRepositorio
    {
        Resultado InsertarD(Dispositivo objDispositivo);
        Resultado EliminarD(int id);
        Resultado ActualizarD(Dispositivo objDispositivo);
        string obtenerTelefono(int id);
        List<Dispositivo> ListaDispositivos();
        List<Dispositivo> ListaDispositivosActivos();
    }
}
