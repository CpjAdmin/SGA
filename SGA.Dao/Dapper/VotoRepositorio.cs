using Dapper;
using SGA.Dao.Repositorio;
using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Dao.Dapper
{
    public class VotoRepositorio : IVotoRepositorio
    {
        #region Metodos
        public bool Actualizar(Voto voto)
        {
            throw new NotImplementedException();
        }

        public bool Agregar(Voto voto)
        {
            try
            {
                string cod_proceso = "C";

                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", voto.Id_eleccion);
                parametros.Add("@id_ronda", voto.Id_ronda);
                parametros.Add("@id_papeleta", voto.Id_papeleta);
                parametros.Add("@id_delegado", voto.Id_delegado);

                parametros.Add("@lista_candidatos", voto.Lista_candidatos);

                parametros.Add("@usuario_ejecuta", voto.Usuario);
                parametros.Add("@terminal", voto.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using(IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista de Votos
                    var resultado = bd.Execute("sp_sga_crud_votos", parametros, commandType: CommandType.StoredProcedure);

                    return resultado > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_votos", ex);
            }
        }

        public List<Voto> BuscarId(int id_eleccion, int id_ronda, int id_papeleta, int Id_delegado)
        {
            throw new NotImplementedException();
        }

        public List<Voto> BuscarTodos(int id_eleccion, int id_ronda, int id_papeleta)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(Voto voto)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
