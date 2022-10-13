using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Repositorio
{
    public interface IGestionRepositorio
    {
        Resultado Ingresar(Gestion objGestion);
        Resultado IngresarC(Gestion objGestion);
        Resultado Actualizar(Gestion objGestion);
        Resultado Eliminar(Gestion objGestion);
        Gestion BuscarId(Gestion objGestion);
        List<Gestion> ListaGestion();
        List<Gestion> ListaGestionC(int id_papeleta);

    }
}
