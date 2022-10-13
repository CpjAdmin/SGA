using Dapper;
using SGA.Dao.Repositorio;
using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGA.Dao.Dapper
{
    public class CandidatoRepositorio : ICandidatoRepositorio
    {
        #region Metodos

        /// <summary>
        /// Actualizar
        /// </summary>
        /// <param name="Candidato"></param>
        /// <returns></returns>
        public bool Actualizar(Candidato Candidato)
        {
            try
            {
                string usuario = "Prueba";
                string terminal = "Terminal Prueba";
                string cod_proceso = "U";

                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", Candidato.Id_eleccion);
                parametros.Add("@id_ronda", Candidato.Id_ronda);
                parametros.Add("@id_papeleta", Candidato.Id_papeleta);
                parametros.Add("@id_candidato", Candidato.Id_candidato);

                parametros.Add("@num_posicion", Candidato.Num_posicion);
                parametros.Add("@descripcion", Candidato.Descripcion);
                parametros.Add("@i_estado", Candidato.I_estado);

                parametros.Add("@usuario", usuario);
                parametros.Add("@terminal", terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista de candidatos
                    var resultado = bd.Execute("sp_sga_crud_papeletas_candidato", parametros, commandType: CommandType.StoredProcedure);

                    return resultado > 0;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Agregar
        /// </summary>
        /// <param name="Candidato"></param>
        /// <returns></returns>
        public bool Agregar(Candidato Candidato)
        {
            try
            {
                string usuario = "Prueba";
                string terminal = "Terminal Prueba";
                string cod_proceso = "C";

                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", Candidato.Id_eleccion);
                parametros.Add("@id_ronda", Candidato.Id_ronda);
                parametros.Add("@id_papeleta", Candidato.Id_papeleta);
                parametros.Add("@id_candidato", Candidato.Id_candidato);

                parametros.Add("@ium_posicion", Candidato.Num_posicion);
                parametros.Add("@descripcion", Candidato.Descripcion);
                parametros.Add("@i_estado", Candidato.I_estado);

                parametros.Add("@usuario", usuario);
                parametros.Add("@terminal", terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista de candidatos
                    var resultado = bd.Execute("sp_sga_crud_papeletas_candidato", parametros, commandType: CommandType.StoredProcedure);

                    return resultado > 0;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Buscar por ID
        /// </summary>
        /// <param name="id_eleccion"></param>
        /// <param name="id_ronda"></param>
        /// <param name="id_papeleta"></param>
        /// <param name="id_candidato"></param>
        /// <returns></returns>
        public Candidato BuscarId(int id_eleccion, int id_ronda, int id_papeleta, int id_candidato)
        {
            try
            {
                string Cod_proceso = "R";

                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", id_eleccion);
                parametros.Add("@id_ronda", id_ronda);
                parametros.Add("@id_papeleta", id_papeleta);
                parametros.Add("@id_candidato", id_candidato);
                parametros.Add("@cod_proceso", Cod_proceso);

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista de candidatos
                    var candidato = bd.QuerySingle<Candidato>("sp_sga_crud_papeletas_candidato", parametros, commandType: CommandType.StoredProcedure);
                    return candidato;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Buscar Todos
        /// </summary>
        /// <param name="id_eleccion"></param>
        /// <param name="id_ronda"></param>
        /// <param name="id_papeleta"></param>
        /// <returns></returns>
        public List<Candidato> BuscarTodos(int id_eleccion, int id_ronda, int id_papeleta)
        {
            try
            {
                int id_candidato = 0;
                string cod_proceso = "B";

                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", id_eleccion);
                parametros.Add("@id_ronda", id_ronda);
                parametros.Add("@id_papeleta", id_papeleta);
                parametros.Add("@id_candidato", id_candidato);
                parametros.Add("@cod_proceso", cod_proceso);

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista de candidatos
                    return bd.Query<Candidato>("sp_sga_crud_papeletas_candidato", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="id_eleccion"></param>
        /// <param name="id_ronda"></param>
        /// <param name="id_papeleta"></param>
        /// <param name="id_candidato"></param>
        /// <returns></returns>
        public bool Eliminar(int id_eleccion, int id_ronda, int id_papeleta, int id_candidato)
        {
            try
            {
                string Cod_proceso = "D";

                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", id_eleccion);
                parametros.Add("@id_ronda", id_ronda);
                parametros.Add("@id_papeleta", id_papeleta);
                parametros.Add("@id_candidato", id_candidato);
                parametros.Add("@cod_proceso", Cod_proceso);

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista de candidatos
                    var resultado = bd.Execute ("sp_sga_crud_papeletas_candidato", parametros, commandType: CommandType.StoredProcedure);

                    return resultado > 0;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        #endregion

    }
}
