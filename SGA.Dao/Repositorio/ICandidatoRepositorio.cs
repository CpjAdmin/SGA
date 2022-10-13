using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    public interface ICandidatoRepositorio
    {
        bool Agregar(Candidato Candidato);
        bool Actualizar(Candidato Candidato);
        Candidato BuscarId(int id_eleccion, int id_ronda, int id_papeleta, int id_candidato);
        List<Candidato> BuscarTodos(int id_eleccion, int id_ronda, int id_papeleta);
        bool Eliminar(int id_eleccion, int id_ronda, int id_papeleta, int id_candidato);
    }
}
