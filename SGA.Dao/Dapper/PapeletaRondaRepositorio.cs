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
    public class PapeletaRondaRepositorio: IPapeletaRondaRepositorio
    {
        private IDbConnection bd;

        #region Metodos
        public Resultado AgregarPR(PapeletaRonda PapeletaR)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = " Papeleta ronda guardada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "C";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", PapeletaR.Id_eleccion);
                parametros.Add("@id_ronda", PapeletaR.Id_ronda);
                parametros.Add("@id_papeleta", PapeletaR.Id_papeleta);
                parametros.Add("@descripcion", PapeletaR.Descripcion);
                parametros.Add("@num_votos", PapeletaR.Num_votos);
                parametros.Add("@orden", PapeletaR.Orden);
                parametros.Add("@i_estado", PapeletaR.I_estado);
                parametros.Add("@usuario", PapeletaR.Usuario);
                parametros.Add("@terminal", PapeletaR.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_papeletas_rondas", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar Papeleta ronda ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar Papeleta ronda: " + ex.Message;
                return resultado;
            }
        }
        public Resultado ActualizarPR(PapeletaRonda PapeletaR)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Papeleta ronda guardada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "U";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", PapeletaR.Id_eleccion);
                parametros.Add("@id_ronda", PapeletaR.Id_ronda);
                parametros.Add("@id_papeleta", PapeletaR.Id_papeleta);
                parametros.Add("@descripcion", PapeletaR.Descripcion);
                parametros.Add("@num_votos", PapeletaR.Num_votos);
                parametros.Add("@orden", PapeletaR.Orden);
                parametros.Add("@i_estado", PapeletaR.I_estado);
                parametros.Add("@usuario", PapeletaR.Usuario);
                parametros.Add("@terminal", PapeletaR.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_papeletas_rondas", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar Papeleta ronda ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar Papeleta ronda: " + ex.Message;
                return resultado;
            }
        }
        public Resultado EliminarPR(PapeletaRonda PapeletaR)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Elección eliminada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "D";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", PapeletaR.Id_eleccion);
                parametros.Add("@id_ronda", PapeletaR.Id_ronda);
                parametros.Add("@id_papeleta", PapeletaR.Id_papeleta);
                parametros.Add("@Descripcion", "ELIMINAR");
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_papeletas_rondas", parametros, commandType: CommandType.StoredProcedure);

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
        //*** VALIDADO - VotacionesResumen.ASPX - Obtiene la lista de Papeletas por Elección
        public List<PapeletaRonda> ListadoPapeletasR(PapeletaRonda PapeletaR)
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", PapeletaR.Id_eleccion);
                parametros.Add("@id_ronda", PapeletaR.Id_ronda);
                parametros.Add("@cod_proceso", "R");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<PapeletaRonda>("sp_sga_crud_papeletas_rondas", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_papeletas_rondas", ex);
            }
        }
        public List<Papeleta> ListadoPapeletas()
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", 0);
                parametros.Add("@id_ronda", 0);
                parametros.Add("@cod_proceso", "P");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<Papeleta>("sp_sga_crud_papeletas_rondas", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_papeletas_rondas", ex);
            }
        }
        public Papeleta PapeletaActual(Papeleta objPapeleta)
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", objPapeleta.Id_eleccion);
                parametros.Add("@id_ronda", objPapeleta.Id_ronda);
                parametros.Add("@id_papeleta", objPapeleta.Id_papeleta);
                parametros.Add("@id_delegado", objPapeleta.Id_delegado);

                parametros.Add("@usuario_ejecuta", objPapeleta.Usuario);
                parametros.Add("@terminal", objPapeleta.Terminal);
                parametros.Add("@cod_proceso", "B");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.QueryFirstOrDefault<Papeleta>("sp_sga_crud_votos", parametros, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_votos", ex);
            }
        }

        #endregion
    }
}
