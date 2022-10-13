using Dapper;
using SGA.Dao.Repositorio;
using SGA.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGA.Dao.Dapper
{
    public class PapeletaCandidatoRepositorio : IPapeletaCandidatoRepositorio
    {
        private IDbConnection bd;

        #region Metodos
        public Resultado AgregarPC(PapeletaCandidato PapeletaC)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = " Papeleta candidato guardada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "C";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", PapeletaC.Id_eleccion);
                parametros.Add("@id_ronda", PapeletaC.Id_ronda);
                parametros.Add("@id_papeleta", PapeletaC.Id_papeleta);
                parametros.Add("@id_candidato", PapeletaC.Id_candidato);
                parametros.Add("@descripcion", PapeletaC.Descripcion);
                parametros.Add("@num_posicion", PapeletaC.Num_posicion);
                parametros.Add("@i_estado", PapeletaC.I_estado);
                parametros.Add("@usuario", PapeletaC.Usuario);
                parametros.Add("@terminal", PapeletaC.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_papeletas_candidato", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar Papeleta candidato ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar Papeleta candidato: " + ex.Message;
                return resultado;
            }
        }

        public Resultado ActualizarPC(PapeletaCandidato PapeletaC)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Papeleta candidato guardada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "U";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", PapeletaC.Id_eleccion);
                parametros.Add("@id_ronda", PapeletaC.Id_ronda);
                parametros.Add("@id_papeleta", PapeletaC.Id_papeleta);
                parametros.Add("@id_candidato", PapeletaC.Id_candidato);
                parametros.Add("@descripcion", PapeletaC.Descripcion);
                parametros.Add("@num_posicion", PapeletaC.Num_posicion);
                parametros.Add("@i_estado", PapeletaC.I_estado);
                parametros.Add("@usuario", PapeletaC.Usuario);
                parametros.Add("@terminal", PapeletaC.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_papeletas_candidato", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al insertar Papeleta candidato ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al insertar Papeleta candidato: " + ex.Message;
                return resultado;
            }
        }

        public Resultado EliminarPC(PapeletaCandidato PapeletaC)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Papeleta candidato eliminada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "D";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", PapeletaC.Id_eleccion);
                parametros.Add("@id_ronda", PapeletaC.Id_ronda);
                parametros.Add("@id_papeleta", PapeletaC.Id_papeleta);
                parametros.Add("@id_candidato", PapeletaC.Id_candidato);
                parametros.Add("@descripcion", "ELIMINAR");
                parametros.Add("@num_posicion", PapeletaC.Num_posicion);
                parametros.Add("@i_estado", PapeletaC.I_estado);
                parametros.Add("@usuario", PapeletaC.Usuario);
                parametros.Add("@terminal", PapeletaC.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_papeletas_candidato", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al eliminar Papeleta candidato";
                    };
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al eliminar Papeleta candidato: " + ex.Message;
                return resultado;
            }
        }

        public List<PapeletaCandidato> Listados(PapeletaCandidato PapeletaC)
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", PapeletaC.Id_eleccion);
                parametros.Add("@id_ronda", PapeletaC.Id_ronda);
                parametros.Add("@id_papeleta", PapeletaC.Id_papeleta);
                parametros.Add("@cod_proceso", "R");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<PapeletaCandidato>("sp_sga_crud_papeletas_candidato", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_papeletas_candidato", ex);
            }
        }

        public List<PapeletaCandidato> Listado(PapeletaCandidato PapeletaC)
        {
            try
            {
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", PapeletaC.Id_eleccion);
                parametros.Add("@id_ronda", PapeletaC.Id_ronda);
                parametros.Add("@id_papeleta", PapeletaC.Id_papeleta);
                parametros.Add("@cod_proceso", "I");

                using (IDbConnection bd = ConexionRepositorio.ConexionSQL())
                {
                    //Retorna la lista 
                    return bd.Query<PapeletaCandidato>("sp_sga_crud_papeletas_candidato", parametros, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_papeletas_candidato", ex);
            }
        }


        public Resultado ActualizarPosicion(PapeletaCandidato PapeletaC)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Asignación de posiciones ejecutada correctamente." };
            try
            {
                bd = ConexionRepositorio.ConexionSQL();
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_eleccion", PapeletaC.Id_eleccion);
                parametros.Add("@id_ronda", PapeletaC.Id_ronda);
                parametros.Add("@id_papeleta", PapeletaC.Id_papeleta);
                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_asigna_orden", parametros, commandType: CommandType.StoredProcedure);

                    if (resp <= 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al ejecutar proceso de actualización de posiciones en la papeleta ";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al ejecutar proceso de actualización de posiciones en la papeleta: " + ex.Message;
                return resultado;
            }
        }

        #endregion

    }
}
