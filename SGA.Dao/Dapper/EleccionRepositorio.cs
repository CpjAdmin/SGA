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
    public class EleccionRepositorio : IEleccionRepositorio
    {
        private IDbConnection bd;

        #region Metodos
        public Resultado AgregarE(Eleccion Eleccion)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Elección guardada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "C";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", Eleccion.Id_eleccion);
                parametros.Add("@nombre", Eleccion.Nombre);
                parametros.Add("@descripcion", Eleccion.Descripcion);
                parametros.Add("@f_inicio", Convert.ToDateTime(Eleccion.F_inicio));
                parametros.Add("@f_final", Convert.ToDateTime(Eleccion.F_final));
                parametros.Add("@i_estado", Eleccion.I_estado);
                parametros.Add("@cierreQ", Eleccion.I_cierre_quorum);
                parametros.Add("@usuario", Eleccion.Usuario);
                parametros.Add("@terminal", Eleccion.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_eleccion", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar elección: ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar elección: " + ex.Message;
                return resultado;
            }
        }

        public Resultado ActualizarE(Eleccion Eleccion)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Elección guardada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "U";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", Eleccion.Id_eleccion);
                parametros.Add("@nombre", Eleccion.Nombre);
                parametros.Add("@descripcion", Eleccion.Descripcion);
                parametros.Add("@f_inicio", Convert.ToDateTime(Eleccion.F_inicio));
                parametros.Add("@f_final", Convert.ToDateTime(Eleccion.F_final));
                parametros.Add("@i_estado", Eleccion.I_estado);
                parametros.Add("@cierreQ", Eleccion.I_cierre_quorum);
                parametros.Add("@usuario", Eleccion.Usuario);
                parametros.Add("@terminal", Eleccion.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_eleccion", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al actualizar elección: ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al actualizar elección: " + ex.Message;
                return resultado;
            }
        }

        public Resultado EliminarE(Eleccion Eleccion)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Elección eliminada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "D";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", Eleccion.Id_eleccion);
                parametros.Add("@Descripcion", "ELIMINAR");
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_eleccion", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al eliminar elección";
                    };
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al eliminar elección: " + ex.Message;
                return resultado;
            }
        }

        //*** VALIDADO - VotacionesProgreso.ASPX - Obtiene la lista Elecciones
        public List<Eleccion> ListadoElecciones()
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", 0);
                parametros.Add("@cod_proceso", "R");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<Eleccion>("sp_sga_crud_eleccion", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_eleccion", ex);
            }
        }

        #endregion


    }
}
