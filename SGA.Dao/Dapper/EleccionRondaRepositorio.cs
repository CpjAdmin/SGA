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
    public class EleccionRondaRepositorio : IEleccionRondaRepositorio
    {
        private IDbConnection bd;

        #region Metodos
        public Resultado AgregarER(EleccionRonda EleccionR)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = " Ronda de Elección guardada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "C";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", EleccionR.Id_eleccion);
                parametros.Add("@id_ronda", EleccionR.Id_ronda);
                parametros.Add("@nombre", EleccionR.Nombre);
                parametros.Add("@descripcion", EleccionR.Descripcion);
                parametros.Add("@f_inicio", Convert.ToDateTime(EleccionR.F_inicio));
                parametros.Add("@f_final", Convert.ToDateTime(EleccionR.F_final));
                parametros.Add("@i_estado", EleccionR.I_estado);
                parametros.Add("@usuario", EleccionR.Usuario);
                parametros.Add("@terminal", EleccionR.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_eleccion_ronda", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar elección ronda: ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar elección ronda: " + ex.Message;
                return resultado;
            }
        }

        public Resultado ActualizarER(EleccionRonda EleccionR)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Elección guardada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "U";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", EleccionR.Id_eleccion);
                parametros.Add("@id_ronda", EleccionR.Id_ronda);
                parametros.Add("@nombre", EleccionR.Nombre);
                parametros.Add("@descripcion", EleccionR.Descripcion);
                parametros.Add("@f_inicio", Convert.ToDateTime(EleccionR.F_inicio));
                parametros.Add("@f_final", Convert.ToDateTime(EleccionR.F_final));
                parametros.Add("@i_estado", EleccionR.I_estado);
                parametros.Add("@usuario", EleccionR.Usuario);
                parametros.Add("@terminal", EleccionR.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_eleccion_ronda", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar elección ronda ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar elección ronda: " + ex.Message;
                return resultado;
            }
        }

        public Resultado EliminarER(EleccionRonda EleccionR)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Elección eliminada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "D";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", EleccionR.Id_eleccion);
                parametros.Add("@id_ronda", EleccionR.Id_ronda);
                parametros.Add("@Descripcion", "ELIMINAR");
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_eleccion_ronda", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al eliminar elección ronda";
                    };
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al eliminar elección ronda: " + ex.Message;
                return resultado;
            }
        }

        //*** VALIDADO - VotacionesProgreso.ASPX - Obtiene la lista de Rondas por Elección
        public List<EleccionRonda> ListadoEleccionesR(EleccionRonda EleccionR)
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", EleccionR.Id_eleccion);
                parametros.Add("@cod_proceso", "R");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<EleccionRonda>("sp_sga_crud_eleccion_ronda", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_eleccion_ronda", ex);
            }
        }

        public List<EleccionRonda> ListadoRondas()
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", "B");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<EleccionRonda>("sp_sga_crud_eleccion_ronda", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_eleccion_ronda", ex);
            }
        }

        #endregion
    }
}
