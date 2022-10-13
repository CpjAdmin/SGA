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
    public class ResultadoRepositorio : IResultadoRepositorio
    {

        #region Metodos
        //*** VALIDADO - VotacionesResumen.ASPX - Cargar los resultados en resumen de votación
        public List<ResultadoEleccion> ResultadoResumen(ResultadoEleccion resultado)
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", resultado.Id_eleccion);
                parametros.Add("@id_ronda", resultado.Id_ronda);
                parametros.Add("@id_papeleta", resultado.Id_papeleta);
                parametros.Add("@cod_proceso", "R1");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<ResultadoEleccion>("sp_sga_resultados", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_resultados", ex);
            }
        }

        //*** VALIDADO - VotacionesProgreso.ASPX - Cargar los resultados en progreso de la votación
        public List<ResultadoProgreso> ResultadoProgreso(ResultadoProgreso resultado)
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", resultado.Id_eleccion);
                parametros.Add("@id_ronda", resultado.Id_ronda);
                parametros.Add("@cod_proceso", "R2");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<ResultadoProgreso>("sp_sga_resultados", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_resultados", ex);
            }
        }

        #endregion
    }
}
