using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    public interface IPapeletaCandidatoRepositorio
    {
        Resultado AgregarPC(PapeletaCandidato Candidato);
        Resultado ActualizarPC(PapeletaCandidato Candidato);
        List<PapeletaCandidato> Listados(PapeletaCandidato PapeletaC);
        List<PapeletaCandidato> Listado(PapeletaCandidato PapeletaC);
        Resultado EliminarPC(PapeletaCandidato Candidato);
        Resultado ActualizarPosicion(PapeletaCandidato Candidato);
    }
}
