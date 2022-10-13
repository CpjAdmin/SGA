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
    public class DelegadoRepositorio : IDelegadosRepositorio
    {

        #region Metodos
        public Resultado Insertar(Delegado objDelegado)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Delegado agregado correctamente." };
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "C";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cedula", objDelegado.Cedula);
                parametros.Add("@nombre", objDelegado.Nombre);
                parametros.Add("@num_paleta", objDelegado.Num_paleta);
                parametros.Add("@institucion", objDelegado.Institucion);
                parametros.Add("@centro", objDelegado.Centro);
                parametros.Add("@lugar_trabajo", objDelegado.Lugar_Trabajo);
                parametros.Add("@tel_celular", objDelegado.Tel_Celular);
                parametros.Add("@email", objDelegado.Email);
                parametros.Add("@foto", objDelegado.Foto);
                parametros.Add("@i_estado", objDelegado.I_estado);
                parametros.Add("@usuario", objDelegado.Usuario);
                parametros.Add("@terminal", objDelegado.Terminal);
                parametros.Add("@i_votar", objDelegado.I_votar);
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_delegados", parametros, commandType: CommandType.StoredProcedure);
                    int res = parametros.Get<int>("@result");

                    if (res == 1) {
                        resultado.Error = res;
                        resultado.Mensaje = "El delegado ya existe como usuario, no se puede modificar. ";
                    }else if (res != 0)
                    {
                        resultado.Error = res;
                        resultado.Mensaje = "No se pudo actualizar el delegado, favor intente nuevamente.";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al actualizar datos del delegado: " + ex.Message;
                return resultado;
            }
        }

        public Resultado Actualizar(Delegado objDelegado)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Delegado actualizado correctamente." };
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "U";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_delegado", objDelegado.Id_delegado);
                parametros.Add("@cedula", objDelegado.Cedula);
                parametros.Add("@nombre", objDelegado.Nombre);
                parametros.Add("@num_paleta", objDelegado.Num_paleta);
                parametros.Add("@institucion", objDelegado.Institucion);
                parametros.Add("@centro", objDelegado.Centro);
                parametros.Add("@lugar_trabajo", objDelegado.Lugar_Trabajo);
                parametros.Add("@tel_celular", objDelegado.Tel_Celular);
                parametros.Add("@email", objDelegado.Email);
                parametros.Add("@foto", objDelegado.Foto);
                parametros.Add("@i_estado", objDelegado.I_estado);
                parametros.Add("@usuario", objDelegado.Usuario);
                parametros.Add("@terminal", objDelegado.Terminal);
                parametros.Add("@i_votar", objDelegado.I_votar);
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_delegados", parametros, commandType: CommandType.StoredProcedure);
                    int res = parametros.Get<int>("@result");

                    if (res == 1)
                    {
                        resultado.Error = res;
                        resultado.Mensaje = "El delegado ya existe como usuario, no se puede modificar. ";
                    }
                    else if (res != 0)
                    {
                        resultado.Error = res;
                        resultado.Mensaje = "No se pudo actualizar el delegado, favor intente nuevamente.";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al actualizar datos del delegado: " + ex.Message;
                return resultado;
            }
        }

        public Resultado Eliminar(int id)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Elección guardada correctamente." };
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "D";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_delegado", id);
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_delegados", parametros, commandType: CommandType.StoredProcedure);
                    int res = parametros.Get<int>("@result");

                    if (res == 1)
                    {
                        resultado.Error = res;
                        resultado.Mensaje = "El delegado ya existe como usuario, no se puede eliminar. ";
                    }
                    else if (res != 0)
                    {
                        resultado.Error = res;
                        resultado.Mensaje = "No se pudo eliminar el delegado, favor intente nuevamente.";
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al actualizar datos del delegado: " + ex.Message;
                return resultado;
            }
        }

        public Delegado BuscarId(Delegado objDelegado)
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "I";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_delegado", objDelegado.Id_delegado);
                parametros.Add("@cod_proceso", cod_proceso);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    
                    return bd.QuerySingle<Delegado>("sp_sga_crud_delegados", parametros, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_delegados", ex);
            }
        }

        public List<Delegado> ListaDelegados() {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_delegado", 0);
                parametros.Add("@cod_proceso", "R");

                using (bd)
                {
                    //Retorna la lista 
                    return bd.Query<Delegado>("sp_sga_crud_delegados", parametros, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("sp_sga_crud_delegados", ex);
            }

        }

        public Resultado Trasladar(Delegado objDelegado)
        {
            Resultado resultado = new Resultado() { Error = 0, Mensaje = "Usuarios asignados correctamente." };
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "T";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@id_delegado", 0);
                parametros.Add("@usuario", objDelegado.Usuario);
                parametros.Add("@terminal", objDelegado.Terminal);
                parametros.Add("@cod_proceso", cod_proceso);

                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (bd)
                {
                    //***Retorna la lista de delegados
                    var regAfectados = bd.Execute("sp_sga_crud_delegados", parametros, commandType: CommandType.StoredProcedure);
                    int valorRetornado = parametros.Get<int>("@result");

                    if (valorRetornado < 0)
                    {
                        resultado.Error = -1;
                        resultado.Mensaje = "Error al trasladar usuarios ";
                    }
                    else
                    {
                        resultado.Mensaje = valorRetornado.ToString() + " Usuarios asignados.";
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Error = -1;
                resultado.Mensaje = "Error al trasladar usuarios: " + ex.Message;
                return resultado;
            }
        }

        //*** VALIDADO - TOTAL DE DELEGADOS 
        public int TotalDelegados()
        {
            try
            {
                IDbConnection bd = ConexionRepositorio.ConexionSQL();
                string cod_proceso = "S";
                //Parametros
                var parametros = new DynamicParameters();
                parametros.Add("@cod_proceso", cod_proceso);
                parametros.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                using (bd)
                {
                    //Retorna la lista de candidatos
                    var resp = bd.Execute("sp_sga_crud_delegados", parametros, commandType: CommandType.StoredProcedure);
                    int res = parametros.Get<int>("@result");
                    return res;
                }

            }
            catch (Exception ex)
            {
                return 0;
                throw new Exception("sp_sga_crud_quorum", ex);
            }

        }

        #endregion
    }
}
