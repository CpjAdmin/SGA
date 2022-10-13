using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    interface IQuorumRepositorio
    {
        int IngresarSE(string cedula, string usuario, string terminal);
        List<Quorum> ListaQuorum(string codproceso);
        Resultado EliminarD(string cedula);
        Resultado AbrirQuorum(string usuario, string terminal);
        Resultado CerrarQuroum(string usuario, string terminal);
        int ValidaQuorum();
    }
}
