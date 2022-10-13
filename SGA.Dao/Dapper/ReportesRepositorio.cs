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
    public class ReportesRepositorio : IReportesRepositorio
    {
        

        public List<Reporte> ListadoReportes(string usuario)
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@usuario", usuario);
                parametros.Add("@cod_proceso", "R");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<Reporte>("sp_sga_crud_reportes", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_reportes", ex);
            }
        }
    }
}
