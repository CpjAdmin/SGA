using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    public interface IVotoRepositorio
    {
        List<Voto> BuscarId(int id_eleccion, int id_ronda, int id_papeleta, int id_usuario);
        List<Voto> BuscarTodos(int id_eleccion, int id_ronda, int id_papeleta);
        bool Agregar(Voto voto);
        bool Actualizar(Voto voto);
        bool Eliminar(Voto voto); 
    }
}
